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

	public async Task<AggregatorModel> GetByLoginAsync(string login)
	{
		var aggregatorByLogin = await _databaseContext.Aggregators
			.FirstOrDefaultAsync(x => x.Login.Equals(login));

		return aggregatorByLogin;
	}

	public async Task<AggregatorModel> GetByIdAsync(long id)
	{
		var aggregatorById = await _databaseContext.Aggregators
			.FirstOrDefaultAsync(x => x.Id.Equals(id));

		return aggregatorById;
	}
}