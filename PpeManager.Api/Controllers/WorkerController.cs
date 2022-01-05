using Microsoft.AspNetCore.Mvc;
using PpeManager.Api.Application.Commands.ClosePpePossessionProcessCommand;
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
        public async Task<ActionResult<WorkerDTO>> CreateWorkerAsync([FromBody] CreateWorkerCommand createWorkerCommand, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                if(Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
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

        [HttpPost("upload")]
        public async Task<ActionResult> getData([FromForm]IFormFile file,   [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var command = new ClosePpePossessionProcessCommand(21, file);
                    var identified = new IdentifiedCommand<ClosePpePossessionProcessCommand, bool>(command, guid);
                    var result = await _mediator.Send(identified);
                    return Ok();
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
