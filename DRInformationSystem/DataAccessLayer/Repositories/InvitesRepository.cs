using DataAccessLayer.DbContexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class InvitesRepository : IInvitesRepository
{
	private readonly EntityDbContext _dbContext;

	public InvitesRepository(EntityDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<InviteModel> GetByCodeAsync(string code)
	{
		var inviteModel = await _dbContext.Invites
			.FirstOrDefaultAsync(x => x.Code.Equals(code));

		return inviteModel;
	}

	public async Task<InviteModel> GetLastAsync()
	{
		var inviteModel = await _dbContext.Invites.LastOrDefaultAsync();

		return inviteModel;
	}

	public async Task<InviteModel> GetByIdAsync(long id)
	{
		var inviteModel = await _dbContext.Invites
			.FirstOrDefaultAsync(x => x.Id.Equals(id));

		return inviteModel;
	}

	public async Task SaveAsync(InviteModel invite)
	{
		_dbContext.Invites.Add(invite);

		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(InviteModel invite)
	{
		_dbContext.Invites.Update(invite);

		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(InviteModel invite)
	{
		_dbContext.Invites.Remove(invite);

		await _dbContext.SaveChangesAsync();
	}
}