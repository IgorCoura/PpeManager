namespace PpeManager.Api.Application.Commands.AddNewPpePossessionCommand
{
    public class AddNewPpePossessionCommandHandler : IRequestHandler<AddNewPpePossessionCommand, WorkerDTO>
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly NotificationContext _notificationContext;

        public AddNewPpePossessionCommandHandler(IWorkerRepository workerRepository, NotificationContext notificationContext)
        {
            _workerRepository = workerRepository;
            _notificationContext = notificationContext;
        }

        public async Task<WorkerDTO> Handle(AddNewPpePossessionCommand request, CancellationToken cancellationToken)
        {
            var worker = _workerRepository.Find(x => x.Id == request.WorkerId);
            //var entity = new PpePossession(worker, request.PpeCertificationId, DateOnly.FromDateTime(DateTime.Now), request.Quantity);
            //_notificationContext.AddNotifications(entity.Notifications);

            if (!_notificationContext.IsValid)
                throw new WorkerDomainException();

            //worker.addPpePossession(entity);

            var result = _workerRepository.Update(worker);
            await _workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return WorkerDTO.FromEntity(result);
        }
    }

    public class AddNewPpePossessionIdentifiedCommandHandler : IdentifiedCommandHandler<AddNewPpePossessionCommand, WorkerDTO>
    {
        public AddNewPpePossessionIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }
}
