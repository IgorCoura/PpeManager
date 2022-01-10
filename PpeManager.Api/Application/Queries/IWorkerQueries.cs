namespace PpeManager.Api.Application.Queries
{
    public interface IWorkerQueries
    {
        public IEnumerable<WorkerDTO> GetAll();
    }
}
