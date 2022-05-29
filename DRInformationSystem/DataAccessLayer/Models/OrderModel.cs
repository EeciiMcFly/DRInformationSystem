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

	public AggregatorModel Aggregator { get; set; }

	public List<ResponseModel> Responses { get; set; }

	public List<SheddingModel> Sheddings { get; set; }
}