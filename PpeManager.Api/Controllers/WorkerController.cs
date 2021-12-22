using Microsoft.AspNetCore.Mvc;
using PpeManager.Api.Application.Commands.CreateWorkerCommand;

namespace PpeManager.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
        private readonly IMediator _mediator;

        public WorkerController(
            IMediator mediator
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<ActionResult<WorkerDTO>> CreateWorkerAsync([FromBody] CreateWorkerCommand createWorkerCommand)
        {
            return Created("", await _mediator.Send(createWorkerCommand));
        }
    }
}
