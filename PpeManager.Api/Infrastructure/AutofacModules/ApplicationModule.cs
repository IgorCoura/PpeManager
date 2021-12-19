namespace PpeManager.Api.Infrastructure.AutofacModules;

public class ApplicationModule: Autofac.Module
{

    public ApplicationModule()
    {

    }

    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterType<RequestManager>()
            .As<IRequestManager>()
            .InstancePerLifetimeScope();

        builder.RegisterType<NotificationContext>()
            .InstancePerLifetimeScope();

    }
}
