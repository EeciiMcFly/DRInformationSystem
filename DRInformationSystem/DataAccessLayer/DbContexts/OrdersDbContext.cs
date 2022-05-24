using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public class OrdersDbContext : DbContext
{
	public OrdersDbContext()
	{
	}

	public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<OrderModel> Orders { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (!options.IsConfigured)
		{
			options.UseNpgsql("Host=localhost; Port=5432;Database=inform_system;Username=postgres;Password=masterkey");
		}
	}
}