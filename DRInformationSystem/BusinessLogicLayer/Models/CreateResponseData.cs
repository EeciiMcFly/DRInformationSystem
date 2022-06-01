namespace BusinessLogicLayer.Models;

public class CreateResponseData
{
	public long ConsumerId { get; init; }
	public long OrderId { get; init; }
	public List<long> ReduceData { get; init; }
}