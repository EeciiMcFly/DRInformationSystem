using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IInvitesRepository
{
	public Task<InviteModel> GetInviteByCodeAsync(string code);

	public Task<InviteModel> GetLastInvite();

	public Task SaveInviteAsync(InviteModel invite);

	public Task UpdateInviteAsync(InviteModel invite);
}