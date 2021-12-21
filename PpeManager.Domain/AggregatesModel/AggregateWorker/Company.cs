using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class Company : ValueObject
    {
        public Company(Name name, Cnpj cnpj)
        {
            AddNotifications(
                name.contract,
                cnpj.contract
                );
            if (IsValid)
            {
                Name = name;
                Cnpj = cnpj;
            }
        }

        public Name Name { get; private set; }
        public Cnpj Cnpj { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Cnpj;
        }
    }
}
