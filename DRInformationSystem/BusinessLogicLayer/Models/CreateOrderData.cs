using DataAccessLayer.Models;

namespace BusinessLogicLayer.Models;

public class CreateOrderData
{
	public DateTime PeriodStart { get; set; }
	public DateTime PeriodEnd { get; set; }
	public OrderState State { get; set; }
	public long AggregatorId { get; set; }
}