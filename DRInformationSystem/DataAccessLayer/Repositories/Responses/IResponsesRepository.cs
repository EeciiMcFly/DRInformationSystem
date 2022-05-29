using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IResponsesRepository : ICrudRepository<ResponseModel>
{
	Task<List<ResponseModel>> GetAsync(ResponseSearchParams searchParams);
	Task<List<ResponseModel>> GetRangeByIdsAsync(List<long> responsesId);
}