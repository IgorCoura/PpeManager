namespace PpeManager.Api.Application.Commands.OpenNewPpePossessionProcessCommand
{
    public class OpenNewPpePossessionProcessCommand: IRequest<WorkerDTO>
    {
        public OpenNewPpePossessionProcessCommand(int workerId, IList<Certifications> certifications)
        {
            WorkerId = workerId;
            Certifications = certifications;
        }

        public int WorkerId { get;  set; }
        public IList<Certifications> Certifications { get;  set; }

    }

    public record Certifications
    {
        public Certifications(int ppeCertificationId, int quantity)
        {
            this.ppeCertificationId = ppeCertificationId;
            this.quantity = quantity;
        }       
        public int ppeCertificationId { get; set; }
        public int quantity { get; set; }
    }
}
