using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusinessLogicLayer.Auth;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Utils;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Services;

public class ConsumersService : IConsumersService
{
	private readonly IConsumersRepository _consumersRepository;
	private readonly IInvitesService _invitesService;

	public ConsumersService(IConsumersRepository consumersRepository,
		IInvitesService invitesService)
	{
		_consumersRepository = consumersRepository;
		_invitesService = invitesService;
	}

	public async Task<SecurityToken> AuthorizeConsumerAsync(string login, string password)
	{
		var consumer = await _consumersRepository.GetByLoginAsync(login);

		if (consumer == null)
			throw new BadAuthException();

		var passwordHash = StringHasher.GetSha256Hash(password);
		var passwordIsCorrect = consumer.PasswordHash.Equals(passwordHash);
		if (!passwordIsCorrect)
			throw new BadAuthException();

		var claims = new List<Claim>
		{
			new(ClaimsIdentity.DefaultNameClaimType, consumer.Login),
			new(ClaimsIdentity.DefaultRoleClaimType, AuthOptions.ConsumerRole)
		};
		var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
			ClaimsIdentity.DefaultRoleClaimType);

		var token = new JwtSecurityToken(
			issuer: AuthOptions.ISSUER,
			audience: AuthOptions.AUDIENCE,
			notBefore: DateTime.UtcNow,
			claims: claimsIdentity.Claims,
			expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
			signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
				SecurityAlgorithms.HmacSha256));

		return token;
	}

	public async Task RegisterConsumerAsync(RegisterConsumerData registerData)
	{
		var userByLogin = await _consumersRepository.GetByLoginAsync(registerData.Login);
		if (userByLogin != null)
			throw new AlreadyUsedLoginException(registerData.Login);

		var activationResult = await _invitesService.ActivateInviteCodeAsync(registerData.InviteCode);
		if (activationResult != ActivationResult.Successes)
		{
			if (activationResult == ActivationResult.AlreadyActivated)
				throw new AlreadyActivatedInviteCodeException(registerData.InviteCode);

			throw new CodeNoExistException(registerData.InviteCode);
		}

		var passwordHash = StringHasher.GetSha256Hash(registerData.Password);
		var consumer = new ConsumerModel
		{
			Login = registerData.Login,
			PasswordHash = passwordHash
		};

		await _consumersRepository.SaveAsync(consumer);
	}

	public async Task<List<ConsumerModel>> GetConsumersAsync()
	{
		return await _consumersRepository.GetAsync();
	}

	public async Task<ConsumerModel> GetConsumersByLoginAsync(string login)
	{
		return await _consumersRepository.GetByLoginAsync(login);
	}
}