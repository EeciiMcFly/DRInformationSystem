using Microsoft.IdentityModel.Tokens;

namespace DRInformationSystem.Services;

public interface IAggregatorsService
{
	Task<SecurityToken> AuthorizeAggregatorAsync(string login, string password);
}