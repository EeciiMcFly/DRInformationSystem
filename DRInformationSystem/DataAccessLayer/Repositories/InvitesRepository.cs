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

	public async Task<InviteModel> GetInviteByCodeAsync(string code)
	{
		var inviteModel = await _dbContext.Invites
			.FirstOrDefaultAsync(x => x.Code.Equals(code));

		return inviteModel;
	}

	public async Task<InviteModel> GetLastInvite()
	{
		var inviteModel = await _dbContext.Invites.LastOrDefaultAsync();

		return inviteModel;
	}

	public async Task SaveInviteAsync(InviteModel invite)
	{
		_dbContext.Invites.Add(invite);

		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateInviteAsync(InviteModel invite)
	{
		_dbContext.Invites.Update(invite);

		await _dbContext.SaveChangesAsync();
	}
}