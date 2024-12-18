namespace Report.API.Application.Intefaces;

public interface IMessageSender
{
    void PublishMessage<T>(T message);
}