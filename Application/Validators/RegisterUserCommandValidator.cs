using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterUserCommandValidator :  AbstractValidator<RegisterCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(a => a.RoleName).NotNull().WithMessage("Role Name Can't be null");
            RuleFor(a=>a.FullName).NotNull().WithMessage("Full Name Can't be null");
            RuleFor(a => a.Email)
                .Matches(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email address not valid");

        }
    }
}
