namespace BusinessLogicLayer.Models;

public class CreateOrderData
{
	public DateTime PeriodStart { get; init; }
	public DateTime PeriodEnd { get; init; }
	public long AggregatorId { get; init; }
}