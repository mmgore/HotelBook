using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Report.API.Application.Intefaces;

namespace Report.API.Application.Services;

public class MessageSender : IMessageSender
{
    public async void PublishMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };

        var connection = await factory.CreateConnectionAsync();

        using var channel = await connection.CreateChannelAsync();

        channel.QueueDeclareAsync("report", true, true);
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublishAsync("", "report", body);
    }
}