using Amazon.SQS;
using Aws.Abstractions;
using aws_publisher.Messaging;
using Microsoft.Extensions.Options;

var sqsClient = new AmazonSQSClient();

var measurement = new MeasurementCreated
{
    Id = Guid.NewGuid(),
    Value = 23.5,
    Unit = "Celsius",
    Type = "Temperature",
    DateOf = DateTime.UtcNow
};

var queueSettings = Options.Create(new QueueSettings { QueueName = "measurements" });

var messenger = new SqsMessenger(sqsClient, queueSettings);

var response = await messenger.SendMessageAsync(measurement);

Console.WriteLine();
