using DRInformationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DRInformationSystem.Database;

public class InviteDbContext : DbContext
{
	public InviteDbContext()
	{
	}

	public InviteDbContext(DbContextOptions<InviteDbContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<InviteModel> Invites { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (!options.IsConfigured)
		{
			options.UseNpgsql("Host=localhost; Port=5432;Database=inform_system;Username=postgres;Password=masterkey");
		}
	}
}