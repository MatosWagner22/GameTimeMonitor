using GameTimeMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.DTOs
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIdentifier { get; set; }
        public DeviceStatus Status { get; set; }
        public Guid UserId { get; set; }
    }

    public class CreateDeviceDto
    {
        public string DeviceName { get; set; }
        public string DeviceIdentifier { get; set; }
        public Guid UserId { get; set; }
    }

    public class UpdateDeviceDto
    {
        public string DeviceName { get; set; }
        public DeviceStatus Status { get; set; }
    }
}
