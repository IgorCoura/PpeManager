namespace PpeManager.Api.Application.DTO
{
    public class PpePossessionDTO
    {
        public PpePossessionDTO(bool isDelivered, PpeDTO? ppe = null, int? ppeId = null, string? dueDate = null, List<PossessionRecordDTO>? possessionRecords = null)
        {

            Ppe = ppe;
            PpeId = ppeId;
            IsDelivered = isDelivered;
            DueDate = dueDate;
            PossessionRecords = possessionRecords;
        }
        public PpeDTO? Ppe { get; set; }
        public int? PpeId { get; set; }
        public bool IsDelivered { get; set; }
        public string? DueDate { get; set; }
        public virtual List<PossessionRecordDTO>? PossessionRecords { get; private set; }

        public static PpePossessionDTO FromEntity(PpePossession entity)
        {
            return new PpePossessionDTO(
                entity.IsDelivered,
                entity.Ppe is null ? null : PpeDTO.FromEntity(entity.Ppe),
                entity.PpeId,
                entity.DueDate?.ToString(),
                entity.PossessionRecords?.Select(e => PossessionRecordDTO.FromEntity(e)).ToList());

        }

    }
}
