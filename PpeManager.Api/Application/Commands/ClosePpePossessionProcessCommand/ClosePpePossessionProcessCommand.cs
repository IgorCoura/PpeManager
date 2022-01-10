namespace PpeManager.Api.Application.Commands.ClosePpePossessionProcessCommand
{
    public class ClosePpePossessionProcessCommand : IRequest<WorkerDTO>
    {
        public ClosePpePossessionProcessCommand(int workerId, IFormFile file)
        {
            WorkerId = workerId;
            File = file;
        }

        public int WorkerId { get; set; }
        public IFormFile File { get; set; }
    }
}
