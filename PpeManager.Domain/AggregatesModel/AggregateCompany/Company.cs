namespace PpeManager.Domain.AggregatesModel.AggregateCompany
{
    public class Company : Entity, IAggregateRoot
    {
        public Company(Name name, Cnpj cnpj)
        {
            AddNotifications(
                name.contract,
                cnpj.contract
                );
            Name = name;
            Cnpj = cnpj;
        }

        public Name Name { get; private set; }
        public Cnpj Cnpj { get; private set; }

    }
}
