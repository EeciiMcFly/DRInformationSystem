using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IAggregatorsRepository
{
	//Task<AggregatorModel> GetAggregatorByIdAsync(long id);

	Task<AggregatorModel> GetByLoginAsync(string login);

	//Task SaveAggregatorAsync(AggregatorModel aggregator);

	//Task UpdateAggregatorAsync(AggregatorModel aggregator);

	//Task UpdateAggregatorsRangeAsync(List<AggregatorModel> aggregators);

	//Task DeleteAggregatorByIdAsync(long id);

	//Task DeleteAggregatorAsync(AggregatorModel aggregator);
}