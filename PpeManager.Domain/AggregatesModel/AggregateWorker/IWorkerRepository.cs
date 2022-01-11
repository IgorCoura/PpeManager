namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        public Worker Add(Worker entity);

        public Worker Update(Worker entity);

        public Worker Find(Func<Worker, bool> p);

        public Worker FindByPossession(Func<PpePossession, bool> p);
        public Worker DeeplyFind(Func<Worker, bool> p);
        public IEnumerable<Worker> FindAll(Func<Worker, bool> p);
    }
}
