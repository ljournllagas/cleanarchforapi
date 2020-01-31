using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.AccountName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.AccountNumber)
                .MaximumLength(20)
                .Must(MustBeANumber)
                .WithMessage("Account number must be in numeric value")
                .NotEmpty();
        }

        private bool MustBeANumber(string value)
        {
            if (long.TryParse(value, out _))
            {
                return true;
            }
            return false;
        }
    }
}
