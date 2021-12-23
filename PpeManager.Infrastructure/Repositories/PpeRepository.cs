
namespace PpeManager.Infrastructure.Repositories
{
    public class PpeRepository : IPpeRepository
    {
        private readonly PpeManagerContext _context;

        public PpeRepository(PpeManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        
        public Ppe Add(Ppe entity)
        {

            return _context.Ppe.Add(entity).Entity;

        }
        public Ppe Update(Ppe entity)
        {
            return _context.Ppe.Update(entity).Entity;
        }

        public Ppe Find(Func<Ppe, bool> p)
        {
            var entity = _context.Ppe.Where(p).SingleOrDefault() ?? throw new ArgumentNullException(nameof(Ppe));
            return entity;
        }

        public IEnumerable<Ppe> FindAll(Func<Ppe, bool> p)
        {
            var entity = _context.Ppe.Where(p);
            return entity;
        }
        
    }
}
