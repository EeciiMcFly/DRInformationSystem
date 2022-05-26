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

	// public async Task<AggregatorModel> GetByIdAsync(long id)
	// {
	// 	var aggregatorById = await _databaseContext.Aggregators
	// 		.FirstOrDefaultAsync(x => x.Id == id);
	//
	// 	return aggregatorById;
	// }

	public async Task<AggregatorModel> GetByLoginAsync(string login)
	{
		var aggregatorByLogin = await _databaseContext.Aggregators
			.FirstOrDefaultAsync(x => x.Login.Equals(login));

		return aggregatorByLogin;
	}

	// public async Task SaveAsync(AggregatorModel aggregator)
	// {
	// 	_databaseContext.Aggregators.Add(aggregator);
	//
	// 	await _databaseContext.SaveChangesAsync();
	// }
	//
	// public async Task UpdateAsync(AggregatorModel aggregator)
	// {
	// 	_databaseContext.Aggregators.Update(aggregator);
	//
	// 	await _databaseContext.SaveChangesAsync();
	// }
	//
	// public async Task DeleteAsync(AggregatorModel aggregator)
	// {
	// 	_databaseContext.Aggregators.Remove(aggregator);
	//
	// 	await _databaseContext.SaveChangesAsync();
	// }
}