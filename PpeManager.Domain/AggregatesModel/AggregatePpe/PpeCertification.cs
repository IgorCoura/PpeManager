namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public class PpeCertification: Entity
    {
        public PpeCertification(Ppe ppe, ApprovalCertificate approvalCertificateNumber, DateTime validity, int durability)
        {
            AddNotifications(
                ValidateValidity(validity),
                ValidateDurability(durability),
                approvalCertificateNumber.contract
                );
            Ppe = ppe;
            ApprovalCertificateNumber = approvalCertificateNumber;
            Validity = validity;
            Durability = durability;
        }

        public Ppe Ppe { get; private set; }
        private int _ppeId;
        public ApprovalCertificate ApprovalCertificateNumber { get; private set; }
        public DateTime Validity { get; private set; }
        public int Durability { get; private set; }


        private Contract<Notification> ValidateValidity(DateTime validity) =>
            new Contract<Notification>()
                .IsLowerThan(DateTime.Now, validity, nameof(validity), "Validity is invalid")
                .IsNotNullOrEmpty(nameof(validity), "Validity is invalid");

        private Contract<Notification> ValidateDurability(int durability) =>
            new Contract<Notification>()
                .IsNotNullOrEmpty(nameof(durability), "Durability is invalid")
                .IsLowerOrEqualsThan(0, durability, nameof(durability), "Durability must be greater than 0");

    }
}
