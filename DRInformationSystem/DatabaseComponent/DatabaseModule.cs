using Autofac;
using DatabaseComponent.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseComponent;

public class DatabaseModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.Register(c =>
			{
				var configuration = c.Resolve<IConfiguration>();
				var connectionString = configuration.GetConnectionString("DatabaseConnectionTemplateWithoutDbName");

				var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
				optionsBuilder.UseNpgsql(connectionString);

				var dbContext = new DatabaseContext(optionsBuilder.Options);
				return new AggregatorRepositories(dbContext);
			})
			.As<IAggregatorRepositories>()
			.InstancePerLifetimeScope();
	}
}