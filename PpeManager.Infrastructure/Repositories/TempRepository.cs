
namespace PpeManager.Infrastructure.Repositories
{
    public class TempRepository<T> where T : Entity
    {
        List<T> _list = new List<T>();
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

        public virtual T Find(Func<T, bool> p)
        {
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return default;
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }

        public virtual IEnumerable<T> FindAll(Func<T, bool> p)
        {
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return default;
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }


    }
}
