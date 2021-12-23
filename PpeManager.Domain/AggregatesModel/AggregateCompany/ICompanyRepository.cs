using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.AggregatesModel.AggregateCompany
{
    public interface ICompanyRepository : IRepository<Company>
    {
        public Company Add(Company entity);

        public Company Update(Company entity);

        public Company Find(Func<Company, bool> p);

        public IEnumerable<Company> FindAll(Func<Company, bool> p);
    }
}
