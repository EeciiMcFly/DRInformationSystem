using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts;

public class ResponsesDbContext : DbContext
{
	public ResponsesDbContext()
	{
	}

	public ResponsesDbContext(DbContextOptions<ResponsesDbContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<ResponseModel> Responses { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (!options.IsConfigured)
		{
			options.UseNpgsql("Host=localhost; Port=5432;Database=inform_system;Username=postgres;Password=masterkey");
		}
	}
}