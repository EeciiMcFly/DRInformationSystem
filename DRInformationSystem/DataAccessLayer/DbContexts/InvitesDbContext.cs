using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public class InvitesDbContext : DbContext
{
	public InvitesDbContext()
	{
	}

	public InvitesDbContext(DbContextOptions<InvitesDbContext> options) : base(options)
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