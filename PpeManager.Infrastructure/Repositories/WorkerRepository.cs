
namespace PpeManager.Infrastructure.Repositories
{
    public class WorkerRepository : IWorkerRepository, IWorkerQueries
    {

        private readonly PpeManagerContext _context;

        public WorkerRepository(PpeManagerContext context)
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

        public Worker Add(Worker entity)
        {

            return _context.Worker.Add(entity).Entity;

        }
        public Worker Update(Worker entity)
        {
            return _context.Worker.Update(entity).Entity;
        }

        public Worker Find(Func<Worker, bool> p)
        {
            var entity = _context.Worker.Include(x => x.Company).FirstOrDefault(p) ?? throw new ArgumentNullException(nameof(Worker));
            _context.Entry(entity).Collection(i => i.Ppes).Load();
            _context.Entry(entity).Collection(i => i.PpePossessions).Load();
            return entity;
        }

        public Worker FindByPossession(Func<PpePossession, bool> p)
        {
            var entity = _context.PpePossession.Include(x => x.Worker).FirstOrDefault(p) ?? throw new ArgumentNullException(nameof(PpePossession));
            return _context.Worker.Include(x => x.PpePossessions).Include(x => x.Ppes).FirstOrDefault(x => x.Id == entity.Worker.Id) ?? throw new ArgumentNullException(nameof(Worker));
        }

        public IEnumerable<Worker> FindAll(Func<Worker, bool> p)
        {
            var entity = _context.Worker.Where(p);
            return entity;
        }

    }
}
