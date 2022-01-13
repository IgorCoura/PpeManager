using PpeManager.Domain.AggregatesModel.AggregateCompany;

namespace PpeManager.Api.Application.Commands.CreateWorkerCommand
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, WorkerDTO>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IWorkerRepository _workerRepository;



        public CreateWorkerCommandHandler(NotificationContext notificationContext, IWorkerRepository workerRepository)
        {
            _notificationContext = notificationContext;
            _workerRepository = workerRepository;
        }

        public async Task<WorkerDTO> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Worker(request.Name, request.Role, request.Cpf, request.RegistrationNumber, request.AdmissionDate, request.CompanyId);

            if(request.Ppes != null)
            {
                foreach (var p in request.Ppes)
                {
                    var ppePossession = new PpePossession(p.PpeId);
                    entity.AddPpePossession(ppePossession);
                }                
            }

            _notificationContext.AddNotifications(entity.Notifications);
            if (!_notificationContext.IsValid)
            {
                throw new WorkerDomainException();
            }


            var entityResult = _workerRepository.Add(entity);

            await _workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return WorkerDTO.FromEntity(entityResult);
        }
    }

    public class CreateWorkerIdentifiedCommandHandler : IdentifiedCommandHandler<CreateWorkerCommand, WorkerDTO>
    {
        public CreateWorkerIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }
}
