// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    
    Console.WriteLine($" [x] Received {message}");
};

await channel.BasicConsumeAsync("report", true, consumer);

Console.WriteLine("Hello, World!");
Console.ReadKey();