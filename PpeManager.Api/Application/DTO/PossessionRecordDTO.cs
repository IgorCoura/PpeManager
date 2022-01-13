namespace PpeManager.Api.Application.DTO
{
    public class PossessionRecordDTO
    {
        public PossessionRecordDTO(int id, int? ppeCertificationId, string deliveryDate, string validity, bool confirmation, string? filePath, int quantity)
        {
            PpeCertificationId = ppeCertificationId;
            DeliveryDate = deliveryDate;
            Validity = validity;
            Confirmation = confirmation;
            FilePath = filePath;
            Quantity = quantity;
            Id = id;
        }
        public int Id { get; private set; }
        public int? PpeCertificationId { get; private set; }
        public string DeliveryDate { get; private set; }
        public string Validity { get; private set; }
        public bool Confirmation { get; private set; }
        public string? FilePath { get; set; }
        public int Quantity { get; set; }

        public static PossessionRecordDTO FromEntity(PossessionRecord entity)
        {
            return new PossessionRecordDTO(entity.Id, entity.PpeCertificationId, entity.DeliveryDate.ToString(new CultureInfo("pt-BR")), entity.Validity.ToString(new CultureInfo("pt-BR")), entity.Confirmation, entity.FilePath, entity.Quantity);
        }

    }
}
