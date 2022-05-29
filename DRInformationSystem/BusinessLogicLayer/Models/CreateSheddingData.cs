namespace BusinessLogicLayer.Models;

public class CreateSheddingData
{
	public DateTime StartTime { get; set; }
	public long Duration { get; set; }
	public long Volume { get; set; }
	public long OrderId { get; set; }
	public long ConsumerId { get; set; }
}