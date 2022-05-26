using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ConsumersRepository : IConsumersRepository
{
	private readonly EntityDbContext _dbContext;

	public ConsumersRepository(EntityDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ConsumerModel> GetByLoginAsync(string login)
	{
		var consumer = await _dbContext.Consumers
			.FirstOrDefaultAsync(x => x.Login.Equals(login));

		return consumer;
	}

	public async Task SaveAsync(ConsumerModel consumer)
	{
		_dbContext.Consumers.Add(consumer);

		await _dbContext.SaveChangesAsync();
	}
}