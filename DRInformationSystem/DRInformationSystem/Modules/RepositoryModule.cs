using Autofac;
using DRInformationSystem.Database;
using DRInformationSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DRInformationSystem.Modules;

public class RepositoryModule : Module
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
				return new AggregatorsRepositories(dbContext);
			})
			.As<IAggregatorsRepositories>()
			.InstancePerLifetimeScope();
	}
}