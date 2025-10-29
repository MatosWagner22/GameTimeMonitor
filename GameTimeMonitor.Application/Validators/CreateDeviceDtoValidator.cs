using FluentValidation;
using GameTimeMonitor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Validators
{
    public class CreateDeviceDtoValidator : AbstractValidator<CreateDeviceDto>
    {
        public CreateDeviceDtoValidator()
        {
            RuleFor(x => x.DeviceName).NotEmpty().WithMessage("Device name is required");
            RuleFor(x => x.DeviceIdentifier).NotEmpty().WithMessage("Device identifier is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
