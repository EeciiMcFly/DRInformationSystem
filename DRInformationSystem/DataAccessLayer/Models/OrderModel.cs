using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Index(nameof(Id))]
public class OrderModel
{
	public long Id { get; set; }

	public DateTime StartTimestamp { get; set; }

	public DateTime EndTimestamp { get; set; }

	public OrderState State { get; set; }

	public long AggregatorId { get; set; }
}