namespace PpeManager.Api.Application.Command.CreatePpeCommand
{
    public class CreatePpeCommandHandler : IRequestHandler<CreatePpeCommand, PpeDTO>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPpeRepository _ppeRepository;
      
        public CreatePpeCommandHandler(NotificationContext notificationContext, IPpeRepository ppeRepository)
        {
            _notificationContext = notificationContext;
            _ppeRepository = ppeRepository;
        }

        public Task<PpeDTO> Handle(CreatePpeCommand request, CancellationToken cancellationToken)
        {

            var entity = new Ppe(request.Name, request.Description);
            _notificationContext.AddNotifications(entity.Notifications);
            if (!_notificationContext.IsValid)
                throw new PpeDomainException("Ppe is invalid");

            var entityResult = _ppeRepository.Add(entity);

            var dto = PpeDTO.FromEntity(entityResult);
            return Task.FromResult(dto);

        }

        public class CreatePpeIdentifiedCommandHandler : IdentifiedCommandHandler<CreatePpeCommand, PpeDTO>
        {
            public CreatePpeIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
            {
            }
        }
    }

}