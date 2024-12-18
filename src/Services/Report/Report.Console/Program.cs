// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Console.Infrastructure;
using Report.Console.Models;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/"
};

var connection = await factory.CreateConnectionAsync();

using var channel = await connection.CreateChannelAsync();

channel.QueueDeclareAsync("report", true, false);

var consumer = new AsyncEventingBasicConsumer(channel);
var reportItem = new ReportItem();

consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    try
    {
        reportItem = JsonConvert.DeserializeObject<ReportItem>(message);
        ReportRepository reportRepository = new ReportRepository();
        await reportRepository.UpdateReportAsync(reportItem);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
};

await channel.BasicConsumeAsync("report", true, consumer);



Console.WriteLine("Hello, World!");
Console.ReadKey();