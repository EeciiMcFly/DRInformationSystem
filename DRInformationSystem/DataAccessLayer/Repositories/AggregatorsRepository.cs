using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class AggregatorsRepository : IAggregatorsRepository
{
	private readonly EntityDbContext _databaseContext;

	public AggregatorsRepository(EntityDbContext databaseContext)
	{
		_databaseContext = databaseContext;
	}

	public async Task<AggregatorModel> GetAggregatorByIdAsync(long id)
	{
		var aggregatorById = await _databaseContext.Aggregators
			.FirstOrDefaultAsync(x => x.Id == id);

		return aggregatorById;
	}

	public async Task<AggregatorModel> GetAggregatorByLoginAsync(string login)
	{
		var aggregatorByLogin = await _databaseContext.Aggregators
			.FirstOrDefaultAsync(x => x.Login.Equals(login));

		return aggregatorByLogin;
	}

	public async Task SaveAggregatorAsync(AggregatorModel aggregator)
	{
		_databaseContext.Aggregators.Add(aggregator);

		await _databaseContext.SaveChangesAsync();
	}

	public async Task UpdateAggregatorAsync(AggregatorModel aggregator)
	{
		_databaseContext.Aggregators.Update(aggregator);

		await _databaseContext.SaveChangesAsync();
	}

	public async Task UpdateAggregatorsRangeAsync(List<AggregatorModel> aggregators)
	{
		_databaseContext.Aggregators.UpdateRange(aggregators);

		await _databaseContext.SaveChangesAsync();
	}

	public async Task DeleteAggregatorByIdAsync(long id)
	{
		var aggregator = await GetAggregatorByIdAsync(id);
		_databaseContext.Aggregators.Remove(aggregator);

		await _databaseContext.SaveChangesAsync();
	}

	public async Task DeleteAggregatorAsync(AggregatorModel aggregator)
	{
		_databaseContext.Aggregators.Remove(aggregator);

		await _databaseContext.SaveChangesAsync();
	}
}