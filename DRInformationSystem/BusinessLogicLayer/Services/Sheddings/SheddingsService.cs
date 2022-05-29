using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class SheddingsService : ISheddingsService
{
	private readonly ISheddingsRepository _sheddingsRepository;

	public SheddingsService(ISheddingsRepository sheddingsRepository)
	{
		_sheddingsRepository = sheddingsRepository;
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

	public async Task SetNewStatusForShedding(long sheddingId, SheddingState sheddingState)
	{
		var shedding = await _sheddingsRepository.GetByIdAsync(sheddingId);
		shedding.Status = sheddingState;

		await _sheddingsRepository.UpdateAsync(shedding);
	}

	public async Task DeleteShedding(long sheddingId)
	{
		var shedding = await _sheddingsRepository.GetByIdAsync(sheddingId);

		await _sheddingsRepository.DeleteAsync(shedding);
	}
}