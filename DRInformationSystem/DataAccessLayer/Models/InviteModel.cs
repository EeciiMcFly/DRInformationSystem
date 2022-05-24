using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Index(nameof(Code))]
public class InviteModel
{
	public long Id { get; set; }

	public string Code { get; set; }

	public bool IsActivated { get; set; }

	public long AggregatorId { get; set; }

	public AggregatorModel Aggregator { get; set; }
}