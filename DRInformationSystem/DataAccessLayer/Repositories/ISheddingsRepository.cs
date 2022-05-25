using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface ISheddingsRepository : ICrudRepository<SheddingModel>
{
	Task<List<SheddingModel>> GetByOrderIdAsync(long orderId);
	Task<List<SheddingModel>> GetByOrderIdAndConsumerId(long orderId, long consumerId);
}