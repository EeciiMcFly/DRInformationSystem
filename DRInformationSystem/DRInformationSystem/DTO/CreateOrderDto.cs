namespace DRInformationSystem.DTO;

public class CreateOrderDto
{
	public DateTime PeriodStart { get; set; }
	public DateTime PeriodEnd { get; set; }
	public long AggregatorId { get; set; }
}