using PpeManager.Domain.AggregatesModel.AggregateWorker;
using PpeManager.Domain.Seedwork;

namespace PpeManager.Infrastructure.Repositories
{
    public class WorkerRepository : TempRepository<Worker>, IWorkerRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    }
}
