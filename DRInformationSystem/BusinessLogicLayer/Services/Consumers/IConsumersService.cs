using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Services;

public interface IConsumersService
{
	Task<SecurityToken> AuthorizeConsumerAsync(string login, string password);
	Task RegisterConsumerAsync(RegisterConsumerData registerData);
	Task<List<ConsumerModel>> GetConsumersAsync();
	Task<ConsumerModel> GetConsumersByLoginAsync(string login);
}