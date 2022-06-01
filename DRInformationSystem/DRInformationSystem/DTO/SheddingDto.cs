using DataAccessLayer.Models;

namespace DRInformationSystem.DTO;

public class SheddingDto
{
	public long Id { get; set; }
	public DateTime StartTime { get; set; }
	public long Duration { get; set; }
	public long Volume { get; set; }
	public SheddingState Status { get; set; }
	public long OrderId { get; set; }
	public long ConsumerId { get; set; }
}