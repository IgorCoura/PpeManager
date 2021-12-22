namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class PpePossession: Entity
    {
        public Worker Worker { get; private set; }
        public int getWorkerId => WorkerId;
        private int WorkerId;      
        public PpeCertification PpeCertification { get; private set; }
        public int getPpeCertificationId => PpeCertificationId;
        private int PpeCertificationId;
        public DateOnly DeliveryDate { get; private set; }
        public DateOnly Validity { get; private set; }
        public bool Confirmation { get; private set; } = false;
        public string SupportingDocument { get; set; } = "";
        public int Quantity { get; set; }

        public PpePossession(int workerId, int ppeCertificationId, DateOnly deliveryDate, int quantity)
        {
            AddNotifications(
                ValidateQuantity(quantity),
                ValidateDeliveryDate(deliveryDate)
                );

            if (IsValid)
            {                
                DeliveryDate = deliveryDate;              
                Quantity = quantity;
                WorkerId = workerId;
                PpeCertificationId = ppeCertificationId;
            }

            EventSetValidity();
            
        }

        public void setValidity(DateOnly value)
        {
            AddNotifications(
                ValidateValidity(value)
                );
            if (IsValid)
            {
                Validity = value;
            }
        }

        public void EventSetValidity()
        {
            AddDomainEvent(new SetValidityToPpePossession(Id, PpeCertificationId));
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
