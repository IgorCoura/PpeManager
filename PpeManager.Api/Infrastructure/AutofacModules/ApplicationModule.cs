namespace PpeManager.Api.Infrastructure.AutofacModules;

public class ApplicationModule : Autofac.Module
{
    private readonly string _queriesConnectionString;

    public ApplicationModule(string queriesConnectionString)
    {
        _queriesConnectionString = queriesConnectionString;
    }



    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WorkerQueries>()
            .As<IWorkerQueries>()
            .InstancePerLifetimeScope();    

        builder.RegisterType<RequestManager>()
            .As<IRequestManager>()
            .InstancePerLifetimeScope();

        builder.RegisterType<NotificationContext>()
            .InstancePerLifetimeScope();

        builder.RegisterType<PpeRepository>()
            .As<IPpeRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CompanyRepository>()
            .As<ICompanyRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<WorkerRepository>()
            .As<IWorkerRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ConsultApprovalCertificateNumberService>()
            .As<IConsultApprovalCertificateNumberService>()
            .InstancePerLifetimeScope();

        builder.Register(x => new HttpClient() { Timeout = TimeSpan.FromSeconds(10) })
            .SingleInstance();

    }
}
