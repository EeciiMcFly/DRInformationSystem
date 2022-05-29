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
		return await _dbContext.Orders.ToListAsync();
	}

	public async Task<List<OrderModel>> GetAsync(OrderSearchParams searchParams)
	{
		var query = _dbContext.Orders
			.Include(x => x.Responses)
			.Include(x => x.Sheddings)
			.AsQueryable();

		var isAggregatorFilterExist = searchParams.AggregatorId.HasValue;
		var isConsumerFilterExist = searchParams.ConsumerId.HasValue;

		if (isAggregatorFilterExist)
		{
			query = query.Where(x => x.AggregatorId == searchParams.AggregatorId.Value);
		}

		if (isConsumerFilterExist)
		{
			query = query.Where(x => x.Responses.Any(t => t.ConsumerId == searchParams.ConsumerId.Value));
		}

		return await query.ToListAsync();
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