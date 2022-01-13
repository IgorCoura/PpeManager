
namespace PpeManager.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PpeManagerContext _context;

        public CompanyRepository(PpeManagerContext context)
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

        public Company Add(Company entity)
        {

            return _context.Company.Add(entity).Entity;

        }
        public Company Update(Company entity)
        {
            return _context.Company.Update(entity).Entity;
        }

        public Company Find(Func<Company, bool> p)
        {
            var entity = _context.Company.Where(p).SingleOrDefault() ?? throw new ArgumentNullException(nameof(Company));
            return entity;
        }

        public IEnumerable<Company> FindAll(Func<Company, bool> p)
        {
            var entity = _context.Company.Where(p);
            return entity;
        }


    }
}
