using Autofac;
using DataAccessLayer.DbContexts;
using DataAccessLayer.Repositories;
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

				var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>();
				optionsBuilder.UseNpgsql(connectionString);

				var dbContext = new EntityDbContext(optionsBuilder.Options);

				return dbContext;
			})
			.As<EntityDbContext>()
			.InstancePerLifetimeScope();

		builder.RegisterType<AggregatorsRepository>()
			.As<IAggregatorsRepository>()
			.InstancePerLifetimeScope();

		builder.RegisterType<ConsumersRepository>()
			.As<ConsumersRepository>()
			.InstancePerLifetimeScope();

		builder.RegisterType<InvitesRepository>()
			.As<InvitesRepository>()
			.InstancePerLifetimeScope();

		builder.RegisterType<OrdersRepository>()
			.As<IOrdersRepository>()
			.InstancePerLifetimeScope();

		builder.RegisterType<ResponsesRepository>()
			.As<IResponsesRepository>()
			.InstancePerLifetimeScope();

		builder.RegisterType<SheddingsRepository>()
			.As<ISheddingsRepository>()
			.InstancePerLifetimeScope();
	}
}