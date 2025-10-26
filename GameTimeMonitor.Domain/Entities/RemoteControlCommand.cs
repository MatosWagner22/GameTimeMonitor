using System.Data;

namespace GameTimeMonitor.Domain.Entities
{
    public class RemoteControlCommand
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }
        public CommandType Command { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool Executed { get; set; }
    }
}
