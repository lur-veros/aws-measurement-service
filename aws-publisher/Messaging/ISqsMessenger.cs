using Amazon.SQS.Model;

namespace aws_publisher.Messaging;

public interface ISqsMessenger
{
    Task<SendMessageResponse> SendMessageAsync<T>(T message);
}