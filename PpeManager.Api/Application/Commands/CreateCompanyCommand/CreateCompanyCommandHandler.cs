using PpeManager.Domain.AggregatesModel.AggregateCompany;

namespace PpeManager.Api.Application.Commands.CreateCompanyCommand
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDTO>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyCommandHandler(NotificationContext notificationContext, ICompanyRepository companyRepository)
        {
            _notificationContext = notificationContext;
            _companyRepository = companyRepository;
        }

        public Task<CompanyDTO> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = new Company(request.Name, request.Cnpj);
            _notificationContext.AddNotifications(entity.Notifications);
            if (!_notificationContext.IsValid)
            {
                throw new CompanyDomainException();
            }

            var entityResult = _companyRepository.Add(entity);


            return Task.FromResult(CompanyDTO.FromEntity(entityResult));
        }
    }

    public class CreateCompanyIdetifiedCommandHandler : IdentifiedCommandHandler<CreateCompanyCommand, CompanyDTO>
    {
        public CreateCompanyIdetifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }
}
