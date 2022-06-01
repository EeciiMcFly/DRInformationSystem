namespace BusinessLogicLayer.Models;

public class CreateSheddingData
{
	public DateTime StartTime { get; init; }
	public long Duration { get; init; }
	public long Volume { get; init; }
	public long OrderId { get; init; }
	public long ConsumerId { get; init; }
}