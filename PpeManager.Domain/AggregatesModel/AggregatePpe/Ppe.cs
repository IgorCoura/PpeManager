using PpeManager.Domain.AggregatesModel.AggregateWorker;

namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public class Ppe: Entity, IAggregateRoot
    {
        public Name Name { get; }
        public Description Description { get; }
        public virtual IList<PpeCertification> PpeCertifications { get; private set; }

        public virtual IList<Worker> Workers { get; private set; }

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
                PpeCertifications = new List<PpeCertification>();
            }

           
        }

        public void setPpeCertifications(IList<PpeCertification> list)
        {
            PpeCertifications = list;
        }


        public void addCertification(PpeCertification ppe)
        {
            if (ppe.IsValid)
            {
                PpeCertifications.Add(ppe);
            }
            else
            {
                AddNotification(new Notification(nameof(PpeCertification), "Ppe certification is invalid"));
            }
            
        }
     
        
    }
}
