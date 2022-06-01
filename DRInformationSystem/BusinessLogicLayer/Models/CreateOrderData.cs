namespace BusinessLogicLayer.Models;

public class CreateOrderData
{
	public DateTime PeriodStart { get; set; }
	public DateTime PeriodEnd { get; set; }
	public long AggregatorId { get; set; }
}