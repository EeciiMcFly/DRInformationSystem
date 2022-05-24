using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Index(nameof(Id))]
public class SheddingModel
{
	public long Id { get; set; }

	public DateTime StartTimestamp { get; set; }

	public long Duration { get; set; }

	public long Volume { get; set; }

	public SheddingState Status { get; set; }

	public long OrderId { get; set; }

	public long ConsumerId { get; set; }
}