using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;

namespace aws_publisher.Messaging;

public class SqsMessenger : ISqsMessenger
{
    private readonly IAmazonSQS _sqs;
    private readonly IOptions<QueueSettings> _queueSettings;    

    public SqsMessenger(IAmazonSQS sqs, IOptions<QueueSettings> queueSettings)
    {
        _sqs = sqs;
        _queueSettings = queueSettings;
    }

    public async Task<SendMessageResponse> SendMessageAsync<T>(T message)
    {
        var queueUrlResponse = await _sqs.GetQueueUrlAsync(_queueSettings.Value.QueueName);

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        return await _sqs.SendMessageAsync(sendMessageRequest);        
    }
}