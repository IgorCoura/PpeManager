namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public class PpeCertification : Entity
    {
#pragma warning disable CS8618 // O propriedade não anulável 'Ppe' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public PpeCertification(int ppeId, ApprovalCertificate approvalCertificateNumber, DateOnly validity, int durability)
#pragma warning restore CS8618 // O propriedade não anulável 'Ppe' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        {
            AddNotifications(
                ValidateValidity(validity),
                ValidateDurability(durability),
                approvalCertificateNumber.contract
                );
            if (IsValid)
            {
                _ppeId = ppeId;
                ApprovalCertificateNumber = approvalCertificateNumber;
                Validity = validity;
                Durability = durability;
            }
        }

        public virtual Ppe Ppe { get; private set; }
        private int _ppeId;
        public int getPpeId => _ppeId;
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
