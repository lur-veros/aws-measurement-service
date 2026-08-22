using System.Text.Json;
using Amazon.Runtime.Internal.Transform;
using Amazon.SQS;
using Amazon.SQS.Model;
using aws_publisher;

var sqsClient = new AmazonSQSClient();

var measurement = new MeasurementCreated
{
    Id = Guid.NewGuid(),
    Value = 23.5,
    Unit = "Celsius",
    Type = "Temperature",
    DateOf = DateTime.UtcNow
};


var queueUrlResponse = await sqsClient.GetQueueUrlAsync("measurements");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(measurement),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(MeasurementCreated)
            } 
        }
    }
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();