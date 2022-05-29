using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services;

public interface IOrdersService
{
	Task<List<OrderModel>> GetOrdersForAggregatorAsync(long aggregatorId);
	Task<List<OrderModel>> GetOrdersForConsumersAsync(long consumerIs);
	Task CreateOrder(CreateOrderData orderData);
	Task CompleteOrder(long orderId, List<long> responsesId);
	Task DeleteOrder(long orderId);
}