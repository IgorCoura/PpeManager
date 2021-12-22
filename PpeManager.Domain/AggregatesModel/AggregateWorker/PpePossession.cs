using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class PpePossession: Entity
    {
        public Worker Worker { get; private set; }
        private int WorkerID;      
        public PpeCertification PpeCertification { get; private set; }
        private int PpeCertificationId;
        public DateOnly DeliveryDate { get; private set; }
        public DateOnly Validity { get; private set; }
        public bool Confirmation { get; private set; }
        public string SupportingDocument { get; set; }
        public int Quantity { get; set; }

        public PpePossession(Worker worker, PpeCertification ppeCertification, DateOnly deliveryDate, DateOnly validity, bool confirmation, string supportingDocument, int quantity)
        {
            AddNotifications(
                ValidateQuantity(quantity),
                ValidateDeliveryDate(deliveryDate),
                ValidateValidity(validity)
                );

            if (IsValid)
            {                
                DeliveryDate = deliveryDate;
                Validity = validity;                
                Quantity = quantity;
                Confirmation = confirmation;
                SupportingDocument = supportingDocument;
                Worker = worker;
                PpeCertification = ppeCertification;
            }
            
        }

        private Contract<Notification> ValidateQuantity(int quantity) =>
            new Contract<Notification>()
                .IsNullOrEmpty(nameof(quantity), "Quantity not be null")
                .IsLowerOrEqualsThan(0, quantity, nameof(quantity), "Quantity must have larger than 0");

        private Contract<Notification> ValidateDeliveryDate(DateOnly deliveryDate) =>
            new Contract<Notification>()
                .IsNullOrEmpty(nameof(deliveryDate), "Delivery Date not be null")
                .IsLowerThan(DateTime.Now.AddDays(-7), deliveryDate.ToDateTime(TimeOnly.MinValue), "Delivery Date is invalid")
                .IsGreaterThan(DateTime.Now.AddDays(7), deliveryDate.ToDateTime(TimeOnly.MinValue), "Delivery Date is invalid");

        private Contract<Notification> ValidateValidity(DateOnly validity) =>
            new Contract<Notification>()
                .IsNullOrEmpty(nameof(validity), "Validity Date not be null")
                .IsLowerThan(DateTime.Now, validity.ToDateTime(TimeOnly.MinValue), "Validity Date is invalid")
                .IsGreaterThan(DateTime.Now.AddYears(100), validity.ToDateTime(TimeOnly.MinValue), "Validity Date is invalid");

    
    }
}
