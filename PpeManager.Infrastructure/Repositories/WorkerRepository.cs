

namespace PpeManager.Infrastructure.Repositories
{
    public class WorkerRepository : TempRepository<Worker>, IWorkerRepository
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
        /*
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
            var entity = _context.Worker.Where(p).SingleOrDefault()?? throw new ArgumentNullException(nameof(Worker));
            return entity;
        }

        public IEnumerable<Worker> FindAll(Func<Worker, bool> p)
        {
            var entity = _context.Worker.Where(p);
            return entity;
        }
        */
    }
}
