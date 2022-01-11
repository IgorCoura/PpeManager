namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public class PpeCertification : Entity
    {
        public PpeCertification( ApprovalCertificate approvalCertificateNumber, DateOnly validity, int durability)
        {
            AddNotifications(
                ValidateValidity(validity),
                ValidateDurability(durability),
                approvalCertificateNumber.contract
                );
            ApprovalCertificateNumber = approvalCertificateNumber;
            Validity = validity;
            Durability = durability;
        }

        public virtual Ppe Ppe { get; private set; }
        public int PpeId { get; private set; }
        public ApprovalCertificate ApprovalCertificateNumber { get; private set; }
        public DateOnly Validity { get; private set; }
        public int Durability { get; private set; }

        public DateOnly getValidityToPpePossession()
        {

            if (Validity.ToDateTime(TimeOnly.MinValue) < DateTime.Now.AddDays(Durability))
            {
                return Validity;
            }
            else
            {
                return  DateOnly.FromDateTime(DateTime.Now.AddDays(Durability));
            }
        }


        private Contract<Notification> ValidateValidity(DateOnly validity) =>
            new Contract<Notification>()
                .IsLowerThan(DateTime.Now, validity.ToDateTime(TimeOnly.MinValue), nameof(validity), "Validity has an invalid date")
                .IsNotNull(validity, nameof(validity), "Validity not be null");

        private Contract<Notification> ValidateDurability(int durability) =>
            new Contract<Notification>()
                .IsNotNull(durability, nameof(durability), "Durability not be null")
                .IsLowerOrEqualsThan(0, durability, nameof(durability), "Durability must be greater than 0");

    }
}
