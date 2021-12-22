using PpeManager.Api.Infrastructure.Services;
using PpeManager.Domain.AggregatesModel.AggregateCompany;

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

        builder.RegisterType<PpeRepository>()
            .As<IPpeRepository>()
            .SingleInstance();

        builder.RegisterType<CompanyRepository>()
            .As<ICompanyRepository>()
            .SingleInstance();

        builder.RegisterType<WorkerRepository>()
            .As<IWorkerRepository>()
            .SingleInstance();

        builder.RegisterType<ConsultApprovalCertificateNumberService>()
            .As<IConsultApprovalCertificateNumberService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<HttpClient>()
            .SingleInstance();

    }
}
