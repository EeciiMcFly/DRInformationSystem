using DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Services;

public interface IAggregatorsService
{
	Task<SecurityToken> AuthorizeAggregatorAsync(string login, string password);
	Task<AggregatorModel> GetAggregatorByLoginAsync(string login);
}