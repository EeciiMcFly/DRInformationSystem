using DRInformationSystem.Models;
using DRInformationSystem.Repositories;
using DRInformationSystem.Utils;

namespace DRInformationSystem.Services;

public class InvitesService : IInvitesService
{
	private readonly IInvitesRepository _invitesRepository;

	public InvitesService(IInvitesRepository invitesRepository)
	{
		_invitesRepository = invitesRepository;
	}

	public async Task<bool> InviteConsumerAsync(string email)
	{
		var lastInvite = await _invitesRepository.GetLastInvite();
		var inviteCodeString = string.Empty;
		inviteCodeString = lastInvite == null
			? "1000-1001"
			: CodeGenerator.GenerateInviteCode(lastInvite.Code);
		var newInvite = new InviteModel
		{
			Code = inviteCodeString,
			IsActivated = false
		};

		//отправка email
		await _invitesRepository.SaveInviteAsync(newInvite);

		return true;
	}

	public async Task<ActivationResult> ActivateInviteCodeAsync(string code)
	{
		var invite = await _invitesRepository.GetInviteByCodeAsync(code);
		if (invite == null)
			return ActivationResult.CodeNotExist;

		if (invite.IsActivated)
			return ActivationResult.AlreadyActivated;

		invite.IsActivated = true;
		await _invitesRepository.UpdateInviteAsync(invite);

		return ActivationResult.Successes;
	}
}