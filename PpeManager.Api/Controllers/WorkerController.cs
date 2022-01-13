using PpeManager.Api.Application.Commands.ClosePpePossessionProcessCommand;
using PpeManager.Api.Application.Commands.CreateWorkerCommand;
using PpeManager.Api.Application.Commands.OpenNewPpePossessionProcessCommand;
using PpeManager.Api.Application.Queries;

namespace PpeManager.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWorkerQueries _workerQueries;

        public WorkerController(IMediator mediator, IWorkerQueries workerQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _workerQueries = workerQueries ?? throw new ArgumentNullException(nameof(workerQueries)); 

        }

        [HttpGet]
        public async Task<IActionResult> Get(int offset = 0, int limit = 10)
        {
            try
            {
                var result = _workerQueries.GetByPage(offset,limit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<WorkerDTO>> CreateWorkerAsync([FromBody] CreateWorkerCommand createWorkerCommand, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var identified = new IdentifiedCommand<CreateWorkerCommand, WorkerDTO>(createWorkerCommand, guid);
                    var result = await _mediator.Send(identified);
                    return Created("", result);
                }
                else
                {
                    return BadRequest("Invalid request Id");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("possession/close")]
        public async Task<ActionResult<WorkerDTO>> ClosePpePossessionProcess([FromForm] IFormFile file, [FromQuery] int workerId, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var command = new ClosePpePossessionProcessCommand(workerId, file);
                    var identified = new IdentifiedCommand<ClosePpePossessionProcessCommand, WorkerDTO>(command, guid);
                    var result = await _mediator.Send(identified);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Invalid request Id");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("possession/open")]
        public async Task<ActionResult<WorkerDTO>> OpenNewPpePossessionProcess([FromBody] OpenNewPpePossessionProcessCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var identified = new IdentifiedCommand<OpenNewPpePossessionProcessCommand, WorkerDTO>(command, guid);
                    var result = await _mediator.Send(identified);
                    return Created("", result);
                }
                else
                {
                    return BadRequest("Invalid request Id");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
