namespace PpeManager.Api.Application.Queries
{
    public interface IWorkerQueries
    {
        public IEnumerable<WorkerDTO> GetByPage(int offset, int limit);
    }
}
