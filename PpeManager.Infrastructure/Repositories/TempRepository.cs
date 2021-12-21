using PpeManager.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Infrastructure.Repositories
{
    public class TempRepository<T> where T: Entity
    {
        private List<T> _list = new List<T>();

        public TempRepository()
        {

        }

        public virtual T Add(T entity)
        {
            _list.Add(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            _list.RemoveAll(e => e.Id == entity.Id);
            _list.Add(entity);
            return entity;
        }

        public virtual T Find(Predicate<T> p)
        {
            return _list.Find(p)?? throw new ArgumentNullException(nameof(T)+" Not found");
        }

        public virtual IEnumerable<T> FindAll(Predicate<T> p)
        {
            return _list.FindAll(p);
        }


    }
}
