using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public class SheddingsDbContext : DbContext
{
	public SheddingsDbContext()
	{
	}

	public SheddingsDbContext(DbContextOptions<SheddingsDbContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<SheddingModel> Sheddings { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (!options.IsConfigured)
		{
			options.UseNpgsql("Host=localhost; Port=5432;Database=inform_system;Username=postgres;Password=masterkey");
		}
	}
}