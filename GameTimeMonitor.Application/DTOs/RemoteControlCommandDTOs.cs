using GameTimeMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.DTOs
{
    public class RemoteControlCommandDto
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public CommandType Command { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool Executed { get; set; }
    }

    public class CreateRemoteControlCommandDto
    {
        public Guid DeviceId { get; set; }
        public CommandType Command { get; set; }
    }
}
