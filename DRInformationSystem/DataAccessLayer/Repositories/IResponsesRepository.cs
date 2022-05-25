using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IResponsesRepository
{
	Task<ResponseModel> GetByIdAsync(long id);
	Task SaveAsync(ResponseModel response);
	Task UpdateAsync(ResponseModel response);
	Task DeleteAsync(ResponseModel response);
}