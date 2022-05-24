using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public class ConsumersDbContext : DbContext
{
	public ConsumersDbContext()
	{
	}

	public ConsumersDbContext(DbContextOptions<ConsumersDbContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<ConsumerModel> Consumers { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (!options.IsConfigured)
		{
			options.UseNpgsql("Host=localhost; Port=5432;Database=inform_system;Username=postgres;Password=masterkey");
		}
	}
}