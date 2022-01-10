
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


        public Ppe FindById(int id)
        {
            return _context.Ppe.FirstOrDefault(p => p.Id == id) ?? throw new ArgumentException(nameof(Ppe));
        }

        public Ppe Find(Func<Ppe, bool> p)
        {
            var entity = _context.Ppe.FirstOrDefault(p) ?? throw new ArgumentException(nameof(Ppe));
            _context.Entry(entity).Collection(x => x.PpeCertifications).Load();
            return entity;
        }

        public PpeCertification FindCertification(Func<PpeCertification, bool> p)
        {
            var entity = _context.PpeCertification.FirstOrDefault(p) ?? throw new ArgumentException(nameof(PpeCertification));
            return entity;
        }

        public IEnumerable<Ppe> FindAll(Func<Ppe, bool> p)
        {
            var entity = _context.Ppe.Where(p);
            return entity;
        }

    }
}
