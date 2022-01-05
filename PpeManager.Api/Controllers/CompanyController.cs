using Microsoft.AspNetCore.Mvc;
using PpeManager.Api.Application.Commands.CreateCompanyCommand;

namespace PpeManager.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDTO>> CreateCompany([FromBody] CreateCompanyCommand createCompany, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {

                if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                {
                    var identified = new IdentifiedCommand<CreateCompanyCommand, CompanyDTO>(createCompany, guid);

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
    }
}
