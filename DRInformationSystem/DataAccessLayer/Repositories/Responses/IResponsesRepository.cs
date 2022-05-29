using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IResponsesRepository : ICrudRepository<ResponseModel>
{
	Task<List<ResponseModel>> GetRangeByIdsAsync(List<long> responsesId);
}