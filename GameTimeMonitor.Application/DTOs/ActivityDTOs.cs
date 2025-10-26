using GameTimeMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.DTOs
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string ApplicationName { get; set; }
    }

    public class CreateActivityDto
    {
        public Guid DeviceId { get; set; }
        public string ApplicationName { get; set; }
    }

    public class UpdateActivityDto
    {
        public DateTime? EndTime { get; set; }
    }
}
