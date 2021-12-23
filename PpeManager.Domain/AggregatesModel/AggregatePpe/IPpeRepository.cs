using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public interface IPpeRepository: IRepository<Ppe>
    {
        public Ppe Add(Ppe entity);

        public Ppe Update(Ppe entity);         

        public Ppe Find(Func<Ppe, bool> p);

        public IEnumerable<Ppe> FindAll(Func<Ppe, bool> p);
    }
}
