namespace DRInformationSystem.DTO;

public class ResponseDto
{
	public long Id { get; set; }
	public List<long> Schedule { get; set; }
	public long OrderId { get; set; }
	public long ConsumerId { get; set; }
}