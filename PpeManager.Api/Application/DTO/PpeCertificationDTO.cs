namespace PpeManager.Api.Application.DTO
{
    public record PpeCertificationDTO
    {
        public PpeCertificationDTO(int id, string approvalCertificateNumber, string validity, int durability)
        {
            Id = id;
            ApprovalCertificateNumber = approvalCertificateNumber;
            Validity = validity;
            Durability = durability;
        }

        public int Id { get; private set; }
        public string ApprovalCertificateNumber { get; private set; }
        public string Validity { get; private set; }
        public int Durability { get; private set; }

    }
}
