using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.API.Application.Commands;
using PaymentGateway.API.Application.Queries;
using PaymentGateway.Domain;
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
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MakeChargeAsync(
            [FromBody] CreateChargeCommand createChargeCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} ({MerchantId}) ({Amount})",
                nameof(createChargeCommand),
                createChargeCommand.MerchantId,
                createChargeCommand.Amount);

            var charge = await _mediator.Send(createChargeCommand);
            return CreatedAtAction(nameof(GetChargById), new { id = charge.Id }, charge);
        }

        [Route("get")]
        [HttpGet]
        [ProducesResponseType(typeof(Charge), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetChargById(Guid merchentId, Guid id)
        {
            if (merchentId == Guid.Empty)
                return BadRequest("Invalid MerchantId");

            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var chargeQuery = new ChargeQuery() { MerchantId = merchentId, Id = id };

            _logger.LogInformation(
                "----- Sending query: {QueryName} {MerchantId} {Id}",
                nameof(chargeQuery),
                chargeQuery.MerchantId,
                chargeQuery.Id);

            var charge = await _mediator.Send(chargeQuery);

            if (charge == null)
                return NotFound();

            return Ok(charge);
        }
    }
}
