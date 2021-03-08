using FluentValidation;
using Microsoft.Extensions.Logging;
using PaymentGateway.API.Application.Commands;
using PaymentGateway.Domain;
using System;

namespace PaymentGateway.API.Application.Validations
{
    public class CreateChargeCommandValidator : AbstractValidator<CreateChargeCommand>
    {
        public CreateChargeCommandValidator(ILogger<CreateChargeCommandValidator> logger)
        {
            RuleFor(command => command.Amount).InclusiveBetween(Constants.CHARGE_AMOUNT_MIN, Constants.CHARGE_AMOUNT_MAX);
            RuleFor(command => command.CurrencyCode).IsInEnum();
            RuleFor(command => command.Brand).IsInEnum();
            RuleFor(command => command.ExpiryMonth).InclusiveBetween(1, 12);
            RuleFor(command => command.ExpiryYear).NotEmpty().Must(_ => _ >= DateTime.UtcNow.Year)
                .WithMessage("Please specify a valid card expiration year");
            RuleFor(command => command.CardNumber).CreditCard();
            RuleFor(command => command.CVV).NotEmpty();
            RuleFor(command => command.City).NotEmpty();
            RuleFor(command => command.State).NotEmpty();
            RuleFor(command => command.Country).NotEmpty();
        }
    }
}
