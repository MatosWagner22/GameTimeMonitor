using FluentValidation;
using GameTimeMonitor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
            RuleFor(x => x.Role).IsInEnum().WithMessage("Invalid role");
            // Si el rol es Child, entonces ParentId no debe ser null.
            RuleFor(x => x.ParentId).NotNull().When(x => x.Role == Domain.Enums.UserRole.Child).WithMessage("ParentId is required for Child role");
        }
    }
}
