namespace PpeManager.Api.Application.Command.CreatePpeCommand
{
    public class CreatePpeCommandExceptionHandler : IRequestExceptionHandler<CreatePpeCommand, PpeDTO>
    {
        public Task Handle(CreatePpeCommand request, Exception exception, RequestExceptionHandlerState<PpeDTO> state, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

