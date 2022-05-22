using DRInformationSystem.Models;

namespace DRInformationSystem.Repositories;

public interface IInvitesRepository
{
	public Task<InviteModel> GetInviteByCodeAsync(string code);

	public Task<InviteModel> GetLastInvite();

	public Task SaveInviteAsync(InviteModel invite);

	public Task UpdateInviteAsync(InviteModel invite);
}