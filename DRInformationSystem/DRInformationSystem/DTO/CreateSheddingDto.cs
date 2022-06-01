namespace DRInformationSystem.DTO;

public class CreateSheddingDto
{
	public DateTime StartTime { get; set; }
	public int Duration { get; set; }
	public long Volume { get; set; }
	public long OrderId { get; set; }
	public long ConsumerId { get; set; }
}