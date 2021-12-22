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

        public Task<WorkerDTO> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Worker(request.Name, request.Role, request.RegistrationNumber, DateOnly.Parse(request.AdmissionDate), request.CompanyId);
            _notificationContext.AddNotifications(entity.Notifications);
            if (!_notificationContext.IsValid)
            {
                throw new WorkerDomainException();
            }

            var entityResult = _workerRepository.Add(entity);

            return Task.FromResult(WorkerDTO.FromEntity(entityResult));
        }   
    }

    public class CreateWorkerIdentifiedCommandHandler : IdentifiedCommandHandler<CreateWorkerCommand, WorkerDTO>
    {
        public CreateWorkerIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }
}
