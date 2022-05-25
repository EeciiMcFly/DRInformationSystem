using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IInvitesRepository : ICrudRepository<InviteModel>
{
	public Task<InviteModel> GetByCodeAsync(string code);

	public Task<InviteModel> GetLastAsync();
}