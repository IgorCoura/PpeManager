namespace PpeManager.Api.Application.Command.IdentifiedCommand
{
    public class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R> where T : IRequest<R>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;

        public IdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager)
        {
            _mediator = mediator;
            _requestManager = requestManager;
        }


        /// <summary>
        /// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
        /// just enqueues the original inner command.
        /// </summary>
        /// <param name="message">IdentifiedCommand which contains both original command & request ID</param>
        /// <returns>Return value of inner command or default value if request same ID was found</returns>
        public async Task<R> Handle(IdentifiedCommand<T, R> request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _requestManager.ExistAsync(request.Id);
            if (!alreadyExists)
            {
                throw new DuplicateCommandException(nameof(request.Command)+" duplicate");
            }
            else
            {
                var manager = _requestManager.CreateRequestForCommandAsync<T>(request.Id);

                var result = await _mediator.Send(request.Command, cancellationToken);

                manager.Wait();

                return result;
            }

            
        }
    }
}
