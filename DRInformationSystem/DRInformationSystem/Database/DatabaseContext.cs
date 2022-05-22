using DRInformationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DRInformationSystem.Database;

internal class DatabaseContext : DbContext
{
	public DatabaseContext()
	{
	}

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<AggregatorModel> Aggregators { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (!options.IsConfigured)
		{
			options.UseNpgsql("Host=localhost; Port=5432;Database=inform_system;Username=postgres;Password=masterkey");
		}
	}
}