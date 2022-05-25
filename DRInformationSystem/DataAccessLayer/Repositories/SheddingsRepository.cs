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

	public async Task<List<SheddingModel>> GetByOrderIdAsync(long orderId)
	{
		var sheddings = await _dbContext.Sheddings
			.Where(x => x.OrderId.Equals(orderId))
			.ToListAsync();

		return sheddings;
	}

	public async Task<List<SheddingModel>> GetByOrderIdAndConsumerId(long orderId, long consumerId)
	{
		var sheddings = await _dbContext.Sheddings
			.Where(x => x.OrderId.Equals(orderId) && x.ConsumerId.Equals(consumerId))
			.ToListAsync();

		return sheddings;
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