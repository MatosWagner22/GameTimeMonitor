using FluentValidation;
using GameTimeMonitor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Validators
{
    public class CreateRemoteControlCommandDtoValidator : AbstractValidator<CreateRemoteControlCommandDto>
    {
        public CreateRemoteControlCommandDtoValidator()
        {
            RuleFor(x => x.DeviceId).NotEmpty().WithMessage("Device ID is required");
            RuleFor(x => x.Command).IsInEnum().WithMessage("Invalid command type");
        }
    }
}
