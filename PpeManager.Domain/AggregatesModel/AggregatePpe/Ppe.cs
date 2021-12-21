namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public class Ppe: Entity, IAggregateRoot
    {
        public Name Name { get; }
        public Description Description { get; }
        public IList<PpeCertification> ppeCertifications { get; }
        public Ppe(Name name, Description description)
        {
            AddNotifications(
                name.contract,
                description.contract
                );
            if (IsValid)
            {
                Name = name;
                Description = description;
            }
            ppeCertifications = new List<PpeCertification>();
        }

        public void addNewPpeCertification(PpeCertification ppe)
        {
            if (ppe.IsValid)
            {
                ppeCertifications.Add(ppe);
            }
            else
            {
                AddNotification(new Notification(nameof(PpeCertification), "Ppe certification is invalid"));
            }
            
        }
     
        
    }
}
