using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public class AggregatorsDbContext : DbContext
{
	public AggregatorsDbContext()
	{
	}

	public AggregatorsDbContext(DbContextOptions<AggregatorsDbContext> options) : base(options)
	{
		Database.Migrate();
	}

	private DbSet<AggregatorModel> Aggregators { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (!options.IsConfigured)
		{
			options.UseNpgsql("Host=localhost; Port=5432;Database=inform_system;Username=postgres;Password=masterkey");
		}
	}
}