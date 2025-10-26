using GameTimeMonitor.Domain.Enums;

namespace GameTimeMonitor.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIdentifier { get; set; } // Identificador único del dispositivo (por ejemplo, dirección MAC)
        public DeviceStatus Status { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
