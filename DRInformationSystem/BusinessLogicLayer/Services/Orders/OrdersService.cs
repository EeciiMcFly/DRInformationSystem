using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class OrdersService : IOrdersService
{
	private readonly IOrdersRepository _ordersRepository;
	private readonly IResponsesRepository _responsesRepository;

	public OrdersService(IOrdersRepository ordersRepository,
		IResponsesRepository responsesRepository)
	{
		_ordersRepository = ordersRepository;
		_responsesRepository = responsesRepository;
	}

	public async Task<List<OrderModel>> GetOrdersForAggregatorAsync(long aggregatorId)
	{
		var searchParams = new OrderSearchParams
		{
			AggregatorId = aggregatorId
		};

		return await _ordersRepository.GetAsync(searchParams);
	}

	public async Task<List<OrderModel>> GetOrdersForConsumersAsync(long consumerIs)
	{
		var searchParams = new OrderSearchParams
		{
			ConsumerId = consumerIs
		};

		return await _ordersRepository.GetAsync(searchParams);
	}

	public async Task CreateOrder(CreateOrderData orderData)
	{
		var orderModel = new OrderModel
		{
			StartTimestamp = orderData.PeriodStart,
			EndTimestamp = orderData.PeriodEnd,
			State = OrderState.NotFormatted,
			AggregatorId = orderData.AggregatorId
		};

		await _ordersRepository.SaveAsync(orderModel);
	}

	public async Task CompleteOrder(long orderId, List<long> responsesId)
	{
		var order = await _ordersRepository.GetByIdAsync(orderId);
		var responses = await _responsesRepository.GetRangeByIdsAsync(responsesId);
		order.Responses = responses;
		order.State = OrderState.Formatted;
		await _ordersRepository.UpdateAsync(order);
	}

	public async Task DeleteOrder(long orderId)
	{
		var order = await _ordersRepository.GetByIdAsync(orderId);
		await _ordersRepository.DeleteAsync(order);
	}
}