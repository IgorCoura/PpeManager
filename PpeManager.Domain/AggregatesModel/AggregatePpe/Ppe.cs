using PpeManager.Domain.AggregatesModel.AggregateWorker;

namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public class Ppe : Entity, IAggregateRoot
    {
        public Name Name { get; }
        public Description Description { get; }
        public virtual IList<PpeCertification> PpeCertifications { get; private set; }


        public Ppe(Name name, Description description)
        {
            AddNotifications(
                name.contract,
                description.contract
                );
            Name = name;
            Description = description;
            PpeCertifications = new List<PpeCertification>();
        } 

        public void addCertification(PpeCertification ppe)
        {
           AddNotifications(ppe.Notifications);
           PpeCertifications.Add(ppe);        
        }


    }
}
