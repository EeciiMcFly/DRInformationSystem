using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Index(nameof(Id))]
public class ResponseModel
{
	public long Id { get; set; }

	public List<long> ReduceData { get; set; }

	public long OrderId { get; set; }

	public OrderModel Order { get; set; }

	public long ConsumerId { get; set; }

	public ConsumerModel Consumer { get; set; }
}