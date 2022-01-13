namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class PpePossession : Entity
    {
        public PpePossession(int ppeId)
        {
            PpeId = ppeId;
        }

        public PpePossession() { }

        public Worker? Worker { get; private set; }
        public int? WorkerId { get; private set; }
        public Ppe? Ppe { get; private set; }
        public int? PpeId { get; private set; }

        public bool IsDelivered { get; private set; } = false;  

        public DateOnly? DueDate { get; private set; }
        public virtual IList<PossessionRecord>? PossessionRecords { get; private set; }

        public void AddPossessionRecord(PossessionRecord possessionRecord)
        {
            if(PossessionRecords is null)
            {
                PossessionRecords = new List<PossessionRecord>();
            }
            PossessionRecords.Add(possessionRecord);
            IsDelivered = true;
            DueDate = possessionRecord.Validity;
        }


    }
}
