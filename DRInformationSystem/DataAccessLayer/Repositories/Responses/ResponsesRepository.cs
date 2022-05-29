using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ResponsesRepository : IResponsesRepository
{
	private readonly EntityDbContext _dbContext;

	public ResponsesRepository(EntityDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ResponseModel> GetByIdAsync(long id)
	{
		var response = await _dbContext.Responses
			.FirstOrDefaultAsync(x => x.Id.Equals(id));

		return response;
	}

	public async Task<List<ResponseModel>> GetAsync(ResponseSearchParams searchParams)
	{
		var query = _dbContext.Responses
			.Include(x => x.Consumer)
			.Include(x => x.Order)
			.AsQueryable();

		var isOrderIdFilterExist = searchParams.OrderId.HasValue;
		var isConsumerIdFilterExist = searchParams.ConsumerId.HasValue;

		if (isOrderIdFilterExist)
			query = query.Where(x => x.OrderId == searchParams.OrderId.Value);

		if (isConsumerIdFilterExist)
			query = query.Where(x => x.ConsumerId == searchParams.ConsumerId.Value);

		return await query.ToListAsync();
	}

	public async Task<List<ResponseModel>> GetRangeByIdsAsync(List<long> responsesId)
	{
		var response = await _dbContext.Responses
			.Where(x => responsesId.Contains(x.Id))
			.ToListAsync();

		return response;
	}

	public async Task SaveAsync(ResponseModel response)
	{
		_dbContext.Responses.Add(response);

		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(ResponseModel response)
	{
		_dbContext.Responses.Update(response);

		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(ResponseModel response)
	{
		_dbContext.Responses.Remove(response);

		await _dbContext.SaveChangesAsync();
	}
}