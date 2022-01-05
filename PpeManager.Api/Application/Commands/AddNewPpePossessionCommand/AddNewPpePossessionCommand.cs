namespace PpeManager.Api.Application.Commands.AddNewPpePossessionCommand
{
    public class AddNewPpePossessionCommand: IRequest<WorkerDTO>
    {
        public AddNewPpePossessionCommand(int workerId, int ppeCertificationId, int quantity)
        {
            WorkerId = workerId;
            PpeCertificationId = ppeCertificationId;
            Quantity = quantity;
        }

        public int WorkerId { get; set; }
        public int PpeCertificationId { get;  set; }
        public int Quantity { get; set; }
    }
}
