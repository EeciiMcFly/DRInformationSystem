using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IResponsesRepository : ICrudRepository<ResponseModel>
{
	Task<List<ResponseModel>> GetRangeByIdAsync(List<long> responsesId);
}