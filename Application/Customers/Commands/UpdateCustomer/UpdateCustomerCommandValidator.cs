using FluentValidation;

namespace Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(v => v.FirstName)
               .MaximumLength(50)
               .NotEmpty();

            RuleFor(v => v.LastName)
               .MaximumLength(50)
               .NotEmpty();

            RuleFor(v => v.EmailAddress)
                .EmailAddress();

            RuleFor(v => v.Age)
                .GreaterThanOrEqualTo(18)
                .WithMessage("Age of the customer must be 18 and up");
        }
    }
}