using DRInformationSystem.Database;
using DRInformationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DRInformationSystem.Repositories;

public class InvitesRepository : IInvitesRepository
{
	private readonly InviteDbContext _dbContext;

	public InvitesRepository(InviteDbContext dbContext)
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