using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class SheddingsRepository : ISheddingsRepository
{
	private readonly EntityDbContext _dbContext;

	public SheddingsRepository(EntityDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<SheddingModel> GetByIdAsync(long id)
	{
		var shedding = await _dbContext.Sheddings
			.FirstOrDefaultAsync(x => x.Id.Equals(id));

		return shedding;
	}

	public async Task<List<SheddingModel>> GetAsync(SheddingSearchParams searchParams)
	{
		var query = _dbContext.Sheddings
			.Include(x => x.Order)
			.Include(x => x.Consumer)
			.AsQueryable();

		var isOrderFilterExist = searchParams.OrderId.HasValue;
		var isConsumerFilterExist = searchParams.ConsumerId.HasValue;

		if (isOrderFilterExist)
		{
			query = query.Where(x => x.OrderId == searchParams.OrderId.Value);
		}

		if (isConsumerFilterExist)
		{
			query = query.Where(x => x.ConsumerId == searchParams.ConsumerId.Value);
		}

		return await query.ToListAsync();
	}

	public async Task SaveAsync(SheddingModel shedding)
	{
		_dbContext.Sheddings.Add(shedding);

		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(SheddingModel shedding)
	{
		_dbContext.Sheddings.Update(shedding);

		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(SheddingModel shedding)
	{
		_dbContext.Sheddings.Remove(shedding);

		await _dbContext.SaveChangesAsync();
	}
}