namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public class PpeCertification: Entity
    {
        public PpeCertification(Ppe ppe, string approvalCertificateNumber, DateTime validity, int durability)
        {
            Ppe = ppe;
            ApprovalCertificateNumber = approvalCertificateNumber;
            Validity = validity;
            Durability = durability;
        }

        public Ppe Ppe { get; private set; }
        public String ApprovalCertificateNumber { get; private set; }
        public DateTime Validity { get; private set; }
        public int Durability { get; private set; }



    }
}
