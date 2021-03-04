using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.API.Application.Commands;
using System.Net;
using System.Threading.Tasks;

namespace PaymentGateway.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IMediator _mediator;

        public PaymentsController(ILogger<PaymentsController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MakeChargeAsync(
            [FromBody] CreateChargeCommand createChargeCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} ({@Command})",
                nameof(createChargeCommand),
                createChargeCommand);

            var commandResult = await _mediator.Send(createChargeCommand);

            return Ok();
        }
    }
}
