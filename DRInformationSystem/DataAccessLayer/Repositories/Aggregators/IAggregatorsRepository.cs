using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IAggregatorsRepository
{
	Task<AggregatorModel> GetByLoginAsync(string login);

	Task<AggregatorModel> GetByIdAsync(long id);
}