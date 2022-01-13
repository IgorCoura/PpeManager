namespace PpeManager.Domain.AggregatesModel.AggregateCompany
{
    public class Company : Entity, IAggregateRoot
    {
        public Company(Name nickName, Name name, Cnpj cnpj)
        {
            AddNotifications(
                nickName.contract,
                name.contract,
                cnpj.contract
                );
            NickName = nickName;
            Name = name;
            Cnpj = cnpj;
        }

        public Name NickName { get; private set; }
        public Name Name { get; private set; }
        public Cnpj Cnpj { get; private set; }

    }
}
