using Autofac;
using BusinessLogicLayer.Services;

namespace DRInformationSystem.Modules;

public class ServiceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<AggregatorsService>()
			.As<IAggregatorsService>()
			.InstancePerLifetimeScope();

		builder.RegisterType<InvitesService>()
			.As<IInvitesService>()
			.InstancePerLifetimeScope();

		builder.RegisterType<ConsumersService>()
			.As<IConsumersService>()
			.InstancePerLifetimeScope();

		builder.RegisterType<OrdersService>()
			.As<IOrdersService>()
			.InstancePerLifetimeScope();

		builder.RegisterType<ResponsesService>()
			.As<IResponsesService>()
			.InstancePerLifetimeScope();

		builder.RegisterType<SheddingsService>()
			.As<ISheddingsService>()
			.InstancePerLifetimeScope();
	}
}