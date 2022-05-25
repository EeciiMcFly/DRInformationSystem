namespace DataAccessLayer.Repositories;

public interface ICrudRepository<T> where T : class
{
	Task<T> GetByIdAsync(long id);
	Task SaveAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
}