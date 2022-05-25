﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataAccessLayer.Repositories;
using DRInformationSystem.Auth;
using DRInformationSystem.Exceptions;
using DRInformationSystem.Utils;
using Microsoft.IdentityModel.Tokens;

namespace DRInformationSystem.Services;

public class AggregatorsService : IAggregatorsService
{
	private readonly IAggregatorsRepository _aggregatorsRepository;

	public AggregatorsService(IAggregatorsRepository aggregatorsRepository)
	{
		_aggregatorsRepository = aggregatorsRepository;
	}

	public async Task<SecurityToken> AuthorizeAggregatorAsync(string login, string password)
	{
		var aggregatorData = await _aggregatorsRepository.GetAggregatorByLoginAsync(login);

		if (aggregatorData == null)
			throw new BadAuthException();

		var passwordHash = StringHasher.GetSha256Hash(password);
		var passwordIsCorrect = aggregatorData.PasswordHash.Equals(passwordHash);
		if (!passwordIsCorrect)
			throw new BadAuthException();

		var claims = new List<Claim>
		{
			new(ClaimsIdentity.DefaultNameClaimType, aggregatorData.Login),
			new(ClaimsIdentity.DefaultRoleClaimType, "aggregator")
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
}