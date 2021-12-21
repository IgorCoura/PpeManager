namespace PpeManager.Api.Application.Commands.AddNewPpeCertificationCommand
{
    public class AddNewPpeCertificationCommand: IRequest<PpeDTO>
    {
        public AddNewPpeCertificationCommand(int ppeId, string approvalCertificateNumber,  int durability)
        {
            PpeId = ppeId;
            ApprovalCertificateNumber = approvalCertificateNumber;
            Durability = durability;
        }

        public int PpeId { get; private set; }
        public string ApprovalCertificateNumber { get; private set; }
        public int Durability { get; private set; }

    }
}
