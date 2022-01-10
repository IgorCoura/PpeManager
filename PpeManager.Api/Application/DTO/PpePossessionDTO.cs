﻿namespace PpeManager.Api.Application.DTO
{
    public class PpePossessionDTO
    {
        public PpePossessionDTO(int id, int ppeCertificationId, string deliveryDate, string validity, bool confirmation, string supportingDocument, int quantity)
        {
            PpeCertificationId = ppeCertificationId;
            DeliveryDate = deliveryDate;
            Validity = validity;
            Confirmation = confirmation;
            SupportingDocument = supportingDocument;
            Quantity = quantity;
            Id = id;
        }
        public int Id { get; private set; }
        public int PpeCertificationId { get; private set; }
        public string DeliveryDate { get; private set; }
        public string Validity { get; private set; }
        public bool Confirmation { get; private set; }
        public string SupportingDocument { get; set; }
        public int Quantity { get; set; }

        public static PpePossessionDTO? FromEntity(PpePossession ppe)
        {
            return ppe != null ? new PpePossessionDTO(ppe.Id, ppe.PpeCertification.Id, ppe.DeliveryDate.ToString(new CultureInfo("pt-BR")), ppe.Validity.ToString(new CultureInfo("pt-BR")), ppe.Confirmation, ppe.SupportingDocument, ppe.Quantity): null;
        }

    }
}
