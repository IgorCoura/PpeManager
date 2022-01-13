

namespace PpeManager.Api.Application.Queries
{
    public class WorkerQueries : IWorkerQueries
    {
        private readonly PpeManagerContext _context;

        public WorkerQueries(PpeManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IEnumerable<WorkerDTO> GetByPage(int offset, int limit)
        {
            var entity = _context.Worker.OrderByDescending(x => x.PpesNotDelivered).OrderBy(x => x.DueDate).Skip(offset).Take(limit).ToList();
            return entity.Select(e => WorkerDTO.FromEntity(e));
        }
    }
}
    
