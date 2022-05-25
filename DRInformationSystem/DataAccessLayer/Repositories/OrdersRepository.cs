using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class OrdersRepository : IOrdersRepository
{
	private readonly EntityDbContext _dbContext;

	public OrdersRepository(EntityDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<OrderModel> GetByIdAsync(long id)
	{
		var order = await _dbContext.Orders
			.FirstOrDefaultAsync(x => x.Id.Equals(id));

		return order;
	}

	public async Task<List<OrderModel>> GetAsync()
	{
		var orders = await _dbContext.Orders.ToListAsync();

		return orders;
	}

	public async Task<List<OrderModel>> GetByAggregatorIdAsync(long aggregatorId)
	{
		var orders = await _dbContext.Orders
			.Where(x => x.AggregatorId == aggregatorId)
			.ToListAsync();

		return orders;
	}

	public async Task SaveAsync(OrderModel order)
	{
		_dbContext.Orders.Add(order);

		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(OrderModel order)
	{
		_dbContext.Orders.Update(order);

		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(OrderModel order)
	{
		_dbContext.Orders.Remove(order);

		await _dbContext.SaveChangesAsync();
	}
}