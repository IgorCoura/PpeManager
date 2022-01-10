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

        public static PpeCertificationDTO? FromEntity(PpeCertification ppeCertification)
        {
            return ppeCertification != null ? new PpeCertificationDTO(ppeCertification.Id, ppeCertification.ApprovalCertificateNumber.ToString(), ppeCertification.Validity.ToString(new CultureInfo("pt-BR")), ppeCertification.Durability): null;
        }

    }
}
