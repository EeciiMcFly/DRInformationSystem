using DataAccessLayer.Models;

namespace DRInformationSystem.DTO;

public class OrderDto
{
	public long Id { get; set; }

	public DateTime StartTimestamp { get; set; }

	public DateTime EndTimestamp { get; set; }

	public OrderState State { get; set; }

	public long AggregatorId { get; set; }

	public AggregatorDto Aggregator { get; set; }

	public List<ResponseDto> Responses { get; set; }

	public List<SheddingDto> Sheddings { get; set; }
}