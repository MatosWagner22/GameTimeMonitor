using FluentValidation;
using GameTimeMonitor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Validators
{
    public class CreateActivityDtoValidator : AbstractValidator<CreateActivityDto>
    {
        public CreateActivityDtoValidator()
        {
            RuleFor(x => x.DeviceId).NotEmpty().WithMessage("Device ID is required");
            RuleFor(x => x.ApplicationName).NotEmpty().WithMessage("Application name is required");
        }
    }
}
