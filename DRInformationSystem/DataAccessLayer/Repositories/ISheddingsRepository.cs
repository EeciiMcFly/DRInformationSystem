using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface ISheddingsRepository
{
	Task<SheddingModel> GetByIdAsync(long id);
	Task<List<SheddingModel>> GetByOrderIdAsync(long orderId);
	Task<List<SheddingModel>> GetByOrderIdAndConsumerId(long orderId, long consumerId);
	Task SaveAsync(SheddingModel shedding);
	Task UpdateAsync(SheddingModel shedding);
	Task DeleteAsync(SheddingModel shedding);
}