using GameTimeMonitor.Domain.Enums;
using System.Diagnostics;

namespace GameTimeMonitor.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public Guid? ParentId { get; set; } // Si es un hijo, tiene un ParentId, si es padre, es null.
        public virtual User Parent { get; set; }
        public virtual ICollection<User> Children { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
