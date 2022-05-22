using Autofac;
using DRInformationSystem.Services;

namespace DRInformationSystem.Modules;

public class ServiceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<AggregatorsService>()
			.As<IAggregatorsService>()
			.InstancePerLifetimeScope();
	}
}