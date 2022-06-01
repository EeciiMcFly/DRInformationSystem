using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services;

public interface ISheddingsService
{
	Task<List<SheddingModel>> GetSheddingForAggregatorOrder(long orderId);
	Task<List<SheddingModel>> GetSheddingForConsumerOrder(long orderId, long consumerId);
	Task CreateShedding(CreateSheddingData createData);
	Task SetNewStatusForShedding(long sheddingId, SheddingState newSheddingState);
	Task DeleteShedding(long sheddingId);
}