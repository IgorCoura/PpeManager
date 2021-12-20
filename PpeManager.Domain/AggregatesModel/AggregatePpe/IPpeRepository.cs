using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.AggregatesModel.AggregatePpe
{
    public interface IPpeRepository
    {
        public Ppe Add(Ppe entity);

        public Ppe Update(Ppe entity);         

        public Ppe Find(Predicate<Ppe> p);

        public IEnumerable<Ppe> FindAll(Predicate<Ppe> p);
    }
}
