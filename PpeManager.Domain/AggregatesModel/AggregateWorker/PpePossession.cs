namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class PpePossession : Entity
    {
        public virtual Worker? Worker { get; private set; }
        public int? WorkerId { get; private set; }
        public virtual PpeCertification? PpeCertification { get; private set; }
        public int? PpeCertificationId { get; private set; }
        public DateOnly DeliveryDate { get; private set; }
        public DateOnly Validity { get; private set; }
        public bool Confirmation { get; private set; } = false;
        public string SupportingDocument { get; set; } = "";
        public int Quantity { get; set; }


        public PpePossession(PpeCertification ppeCertification, DateOnly deliveryDate, int quantity)
        {
            AddNotifications(
                ValidateQuantity(quantity),
                ValidateDeliveryDate(deliveryDate)
                );

            DeliveryDate = deliveryDate;
            Quantity = quantity;
            PpeCertification = ppeCertification;
            Validity = ppeCertification.getValidityToPpePossession();

        }

        public PpePossession() { }
 

        public void confirmation(bool confirmation, string filePath)
        {
            Confirmation = confirmation;
            SupportingDocument = filePath;
        }



        private Contract<Notification> ValidateQuantity(int quantity) =>
            new Contract<Notification>()
                .IsNotNull(quantity, nameof(quantity), "Quantity not be null")
                .IsLowerOrEqualsThan(0, quantity, nameof(quantity), "Quantity must have larger than 0");

        private Contract<Notification> ValidateDeliveryDate(DateOnly deliveryDate) =>
            new Contract<Notification>()
                .IsNotNull(deliveryDate, nameof(deliveryDate), "Delivery Date not be null")
                .IsLowerThan(DateTime.Now.AddDays(-7), deliveryDate.ToDateTime(TimeOnly.MinValue), "Delivery Date is invalid")
                .IsGreaterThan(DateTime.Now.AddDays(7), deliveryDate.ToDateTime(TimeOnly.MinValue), "Delivery Date is invalid");

        private Contract<Notification> ValidateValidity(DateOnly validity) =>
            new Contract<Notification>()
                .IsNotNull(validity, nameof(validity), "Validity Date not be null")
                .IsLowerThan(DateTime.Now, validity.ToDateTime(TimeOnly.MinValue), "Validity Date is invalid")
                .IsGreaterThan(DateTime.Now.AddYears(100), validity.ToDateTime(TimeOnly.MinValue), "Validity Date is invalid");


    }
}
