using PpeManager.Domain.AggregatesModel.AggregateCompany;
using PpeManager.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Infrastructure.Repositories
{
    public class CompanyRepository : TempRepository<Company>, ICompanyRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    }
}
