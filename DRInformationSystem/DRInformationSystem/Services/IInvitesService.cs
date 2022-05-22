﻿namespace DRInformationSystem.Services;

public interface IInvitesService
{
	public Task<bool> InviteConsumerAsync(string email);

	public Task<ActivationResult> ActivateInviteCodeAsync(string code);
}