namespace PpeManager.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PpeController : Controller
    {
        private readonly IMediator _mediator;

        public PpeController(
            IMediator mediator
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost]
        public async Task<ActionResult<PpeDTO>> CreatePpeAsync([FromBody] CreatePpeCommand createPpeCommand, [FromHeader(Name = "x-requestid")] string requestId)
        {

            try
            {

                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var identified = new IdentifiedCommand<CreatePpeCommand, PpeDTO>(createPpeCommand, guid);

                    return Created("", await _mediator.Send(identified));
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

        [HttpPost("certification")]
        public async Task<ActionResult<PpeDTO>> AddNewPpeCertification([FromBody] AddNewPpeCertificationCommand addNewCertificationCommnad, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var identified = new IdentifiedCommand<AddNewPpeCertificationCommand, PpeDTO>(addNewCertificationCommnad, guid);

                    return Created("/certification", await _mediator.Send(identified));
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
