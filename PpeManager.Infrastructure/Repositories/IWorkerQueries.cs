namespace PpeManager.Infrastructure.Repositories
{
    public interface IWorkerQueries
    {
        public Worker Find(Func<Worker, bool> p);
        public IEnumerable<Worker> FindAll(Func<Worker, bool> p);
    }
}