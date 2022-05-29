namespace BusinessLogicLayer.Models;

public class CreateResponseData
{
	public long ConsumerId { get; set; }
	public long OrderId { get; set; }
	public List<long> ReduceData { get; set; }
}