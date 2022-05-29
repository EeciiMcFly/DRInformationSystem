using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IOrdersRepository : ICrudRepository<OrderModel>
{
	Task<List<OrderModel>> GetAsync();
	Task<List<OrderModel>> GetAsync(OrderSearchParams searchParams);
}