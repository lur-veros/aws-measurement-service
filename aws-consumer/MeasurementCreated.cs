namespace aws_publisher;

public class MeasurementCreated
{
    public required Guid Id { get; init; }

    public required double Value { get; init; }

    public required string Unit { get; init; }

    public required string Type { get; init; }

    public required DateTime DateOf { get; init; }
}
