namespace PpeManager.Api.Application.Commands.AddNewPpePossessionCommand
{
    public class AddNewPpePossessionCommandHandler : IRequestHandler<AddNewPpePossessionCommand, PpePossessionDTO>
    {
        public Task<PpePossessionDTO> Handle(AddNewPpePossessionCommand request, CancellationToken cancellationToken)
        {
            var entity = new PpePossession(request.WorkerId, request.PpeCertificationId, DateOnly.FromDateTime(DateTime.Now), request.Quantity);
            throw new NotImplementedException();
        }
    }
}
