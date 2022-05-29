using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IConsumersRepository : ICrudRepository<ConsumerModel>
{
	Task<ConsumerModel> GetByLoginAsync(string login);

	Task<List<ConsumerModel>> GetAsync();
}