using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class SheddingsService : ISheddingsService
{
	private readonly ISheddingsRepository _sheddingsRepository;
	private readonly IOrdersRepository _ordersRepository;
	private readonly IConsumersRepository _consumersRepository;

	public SheddingsService(ISheddingsRepository sheddingsRepository,
		IOrdersRepository ordersRepository,
		IConsumersRepository consumersRepository)
	{
		_sheddingsRepository = sheddingsRepository;
		_ordersRepository = ordersRepository;
		_consumersRepository = consumersRepository;
	}

	public async Task<List<SheddingModel>> GetSheddingForAggregatorOrder(long orderId)
	{
		var searchParams = new SheddingSearchParams
		{
			OrderId = orderId
		};

		return await _sheddingsRepository.GetAsync(searchParams);
	}

	public async Task<List<SheddingModel>> GetSheddingForConsumerOrder(long orderId, long consumerId)
	{
		var searchParams = new SheddingSearchParams
		{
			OrderId = orderId,
			ConsumerId = consumerId
		};

		return await _sheddingsRepository.GetAsync(searchParams);
	}

	public async Task CreateShedding(CreateSheddingData createData)
	{
		var order = await _ordersRepository.GetByIdAsync(createData.OrderId);
		if (order == null)
			throw new NotExistedOrderException(createData.OrderId);

		var consumer = await _consumersRepository.GetByIdAsync(createData.ConsumerId);
		if (consumer == null)
			throw new NotExistedConsumerException(createData.ConsumerId);

		var shedding = new SheddingModel
		{
			StartTimestamp = createData.StartTime,
			Duration = createData.Duration,
			Volume = createData.Volume,
			OrderId = createData.OrderId,
			Status = SheddingState.Prepared,
			ConsumerId = createData.ConsumerId
		};

		await _sheddingsRepository.SaveAsync(shedding);
	}

	public async Task SetNewStatusForShedding(long sheddingId, SheddingState newSheddingState)
	{
		var shedding = await _sheddingsRepository.GetByIdAsync(sheddingId);
		if (shedding == null)
			throw new NotExistedSheddingException(sheddingId);

		switch (shedding.Status)
		{
			case SheddingState.Planned when newSheddingState != SheddingState.Prepared:
				throw new NoValidNewStateForSheddingException(shedding.Status, newSheddingState);
			case SheddingState.Prepared when newSheddingState != SheddingState.Approved:
				throw new NoValidNewStateForSheddingException(shedding.Status, newSheddingState);
			case SheddingState.Approved when newSheddingState != SheddingState.Confirmed:
				throw new NoValidNewStateForSheddingException(shedding.Status, newSheddingState);
			default:
				shedding.Status = newSheddingState;

				await _sheddingsRepository.UpdateAsync(shedding);
				break;
		}
	}

	public async Task DeleteSheddingAsync(long sheddingId)
	{
		var shedding = await _sheddingsRepository.GetByIdAsync(sheddingId);
		if (shedding == null)
			throw new NotExistedSheddingException(sheddingId);

		await _sheddingsRepository.DeleteAsync(shedding);
	}
}