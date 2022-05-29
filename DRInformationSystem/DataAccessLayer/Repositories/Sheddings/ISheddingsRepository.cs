using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface ISheddingsRepository : ICrudRepository<SheddingModel>
{
	Task<List<SheddingModel>> GetAsync(SheddingSearchParams searchParams);
}