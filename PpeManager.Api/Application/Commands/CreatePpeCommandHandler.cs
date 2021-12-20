using PpeManager.Domain.Exceptions;

namespace PpeManager.Api.Application.Commands
{
    public class CreatePpeCommandHandler : IRequestHandler<CreatePpeCommand, PpeDTO>
    {
        private readonly IMediator _mediator;
        private readonly NotificationContext _notificationContext;
        private readonly IPpeRepository _ppeRepository;
      
        public CreatePpeCommandHandler(IMediator mediator, NotificationContext notificationContext, IPpeRepository ppeRepository)
        {
            _mediator = mediator;
            _notificationContext = notificationContext;
            _ppeRepository = ppeRepository;
        }

        public Task<PpeDTO> Handle(CreatePpeCommand request, CancellationToken cancellationToken)
        {

            var entity = new Ppe(request.Name, request.Description);
            _notificationContext.AddNotifications(entity.Notifications);
            if (!_notificationContext.IsValid)
                throw new PpeDomainException("Ppe is invalid");

            _ppeRepository.Add(entity);

            var dto = new PpeDTO(entity.Id, entity.Name.ToString(), entity.Description.ToString());
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