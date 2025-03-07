﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class PossessionRecord: Entity
    {
        public virtual PpePossession? PpePossession { get; private set; }
        public int? PpePossessionId { get; private set; }
        public virtual PpeCertification? PpeCertification { get; private set; }
        public int? PpeCertificationId { get; private set; }
        public DateOnly DeliveryDate { get; private set; }
        public DateOnly Validity { get; private set; }
        public bool Confirmation { get; private set; } = false;
        public string FilePath { get; private set; } = "";
        public int Quantity { get; private set; }


        public PossessionRecord(PpeCertification ppeCertification, DateOnly deliveryDate, int quantity)
        {
            AddNotifications(
                ValidateQuantity(quantity),
                ValidateDeliveryDate(deliveryDate)
                );

            DeliveryDate = deliveryDate;
            Quantity = quantity;
            PpeCertificationId = ppeCertification.Id;
            PpeCertification = ppeCertification;
            Validity = ppeCertification.getValidityToPpePossession();
        }

        public PossessionRecord()
        {
        }


        public void confirmation(bool confirmation, string filePath)
        {
            Confirmation = confirmation;
            FilePath = filePath;
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
