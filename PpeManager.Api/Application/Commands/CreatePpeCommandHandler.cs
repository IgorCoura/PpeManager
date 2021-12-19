namespace PpeManager.Api.Application.Commands
{
    public class CreatePpeCommandHandler : IRequestHandler<CreatePpeCommand, PpeDTO>
    {
        private readonly IMediator _mediator;
        private readonly NotificationContext _notificationContext;
      
        public CreatePpeCommandHandler(IMediator mediator, NotificationContext notificationContext)
        {
            _mediator = mediator;
            _notificationContext = notificationContext;
        }

        public Task<PpeDTO> Handle(CreatePpeCommand request, CancellationToken cancellationToken)
        {

            var model = new Ppe(request.Name, request.Description);
            _notificationContext.AddNotifications(model.Notifications);
            if (!_notificationContext.IsValid)
                return default;

            //TODO: Insert PPE in repository.



            var dto = new PpeDTO(model.Id, model.Name.ToString(), model.Description.ToString());
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