using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.API.Application.Commands;
using PaymentGateway.API.Application.Queries;
using System;
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MakeChargeAsync(
            [FromBody] CreateChargeCommand createChargeCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {@CommandName} Amount: {@Amount}",
                nameof(createChargeCommand),
                createChargeCommand.Amount);

            var paymentResponse = await _mediator.Send(createChargeCommand);
            return Ok(paymentResponse);
        }

        [Route("get")]
        [HttpGet]
        [ProducesResponseType(typeof(PaymentDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetChargById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var chargeQuery = new ChargeQuery() { Id = id };

            _logger.LogInformation(
                "----- Sending query: {@QueryName} Id: {@Id}",
                nameof(chargeQuery),
                chargeQuery.Id);

            var paymentDetail = await _mediator.Send(chargeQuery);
            if (paymentDetail == null)
                return NotFound();

            return Ok(paymentDetail);
        }
    }
}
