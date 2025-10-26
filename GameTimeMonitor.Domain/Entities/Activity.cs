using GameTimeMonitor.Domain.Enums;

namespace GameTimeMonitor.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ActivityStatus Status { get; set; }
        public string ApplicationName { get; set; } // Nombre de la aplicación o juego
    }
}
