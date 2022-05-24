using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public class EntityDbContext : DbContext
{
	public EntityDbContext()
	{
	}

	public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<AggregatorModel> Aggregators { get; set; }

	public DbSet<InviteModel> Invites { get; set; }

	public DbSet<OrderModel> Orders { get; set; }

	public DbSet<ResponseModel> Responses { get; set; }

	public DbSet<ConsumerModel> Consumers { get; set; }

	public DbSet<SheddingModel> Sheddings { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (!options.IsConfigured)
		{
			options.UseNpgsql("Host=localhost; Port=5432;Database=inform_system;Username=postgres;Password=masterkey");
		}
	}
}