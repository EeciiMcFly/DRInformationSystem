using DatabaseComponent.Models;

namespace DatabaseComponent.Repositories;

public interface IAggregatorRepositories
{
	Task<AggregatorModel> GetAggregatorByIdAsync(long id);

	Task<AggregatorModel> GetAggregatorByLoginAsync(string login);

	Task SaveAggregatorAsync(AggregatorModel aggregator);

	Task UpdateAggregatorAsync(AggregatorModel aggregator);

	Task UpdateAggregatorsRangeAsync(List<AggregatorModel> aggregators);

	Task DeleteAggregatorByIdAsync(long id);

	Task DeleteAggregatorAsync(AggregatorModel aggregator);
}