using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IOrdersRepository
{
	Task<OrderModel> GetByIdAsync(long id);
	Task<List<OrderModel>> GetAsync();
	Task<List<OrderModel>> GetByAggregatorIdAsync(long aggregatorId);
	Task SaveAsync(OrderModel order);
	Task UpdateAsync(OrderModel order);
	Task DeleteAsync(OrderModel order);
}