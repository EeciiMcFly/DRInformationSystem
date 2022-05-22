using DRInformationSystem.Models;

namespace DRInformationSystem.Repositories;

public interface IAggregatorsRepository
{
	Task<AggregatorModel> GetAggregatorByIdAsync(long id);

	Task<AggregatorModel> GetAggregatorByLoginAsync(string login);

	Task SaveAggregatorAsync(AggregatorModel aggregator);

	Task UpdateAggregatorAsync(AggregatorModel aggregator);

	Task UpdateAggregatorsRangeAsync(List<AggregatorModel> aggregators);

	Task DeleteAggregatorByIdAsync(long id);

	Task DeleteAggregatorAsync(AggregatorModel aggregator);
}