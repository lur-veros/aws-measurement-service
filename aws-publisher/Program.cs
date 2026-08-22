using System.Text.Json;
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
    MessageBody = JsonSerializer.Serialize(measurement)
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();