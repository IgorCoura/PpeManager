namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public class Ppe: Entity, IAggregateRoot
    {
        public Name Name { get; }
        public Description Description { get; }
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
            
        }

        
    }
}
