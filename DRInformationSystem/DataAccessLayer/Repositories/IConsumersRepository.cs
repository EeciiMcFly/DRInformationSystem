using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IConsumersRepository
{
	Task<ConsumerModel> GetByLoginAsync(string login);

	Task SaveAsync(ConsumerModel consumer);
}