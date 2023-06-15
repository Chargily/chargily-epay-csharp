using System;

using FluentValidation;

namespace Chargily.Epay.CSharp.Validations
{
    public class PaymentRequestValidator : AbstractValidator<EpayPaymentRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(x => x.Amount)
               .GreaterThanOrEqualTo(75)
               .WithMessage("Payment Amount must be greater or equal to 75.0!");

            RuleFor(x => x.DiscountPercentage)
               .LessThan(100)
               .WithMessage("Discount Percentage must be less than 100%!")
               .GreaterThanOrEqualTo(0)
               .WithMessage("Discount Percentage must be a valid percentage value!");

            RuleFor(x => x.InvoiceNumber)
               .NotEmpty()
               .WithMessage("Invoice Number cannot bet null or empty!");

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Client Email cannot be empty!")
               .EmailAddress()
               .WithMessage("Client Email must be a valid email address!");

            RuleFor(x => x.Name)
               .MinimumLength(3)
               .WithMessage("Client Name minimum length is 3!")
               .NotEmpty()
               .WithMessage("Client Name cannot be empty!");

            RuleFor(x => x.BackUrl)
               .NotEmpty()
               .WithMessage("Back URL is required!")
               .Must(x => Uri.TryCreate(x, UriKind.Absolute, out var _))
               .WithMessage("Back URL must a valid URL!");

            RuleFor(x => x.WebhookUrl)
               .NotEmpty()
               .WithMessage("Webhook URL is required!")
               .Must(x => Uri.TryCreate(x, UriKind.Absolute, out var _))
               .WithMessage("Webhook URL must a valid URL!");

            RuleFor(x => x.PaymentMethod)
               .NotNull()
               .WithMessage("Payment Method is required");
        }
    }
}