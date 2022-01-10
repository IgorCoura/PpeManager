

namespace PpeManager.Api.Application.Queries
{
    public class WorkerQueries : IWorkerQueries
    {
        private readonly PpeManagerContext _context;

        public WorkerQueries(PpeManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IEnumerable<WorkerDTO> GetAll()
        {
            var entity = _context.Worker.Include(x => x.Company).Include(x => x.Ppes).Include(x => x.PpePossessions.OrderBy(x => x.Validity)).ThenInclude(x => x.PpeCertification).ToList();
            return entity.Select(e => WorkerDTO.FromEntity(e));
        }
            }

  

}
    
