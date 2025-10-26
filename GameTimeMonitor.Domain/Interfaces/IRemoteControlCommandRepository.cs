using GameTimeMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Domain.Interfaces
{
    public interface IRemoteControlCommandRepository
    {
        Task<RemoteControlCommand> GetByIdAsync(Guid id);
        Task<IEnumerable<RemoteControlCommand>> GetAllAsync();
        Task<RemoteControlCommand> AddAsync(RemoteControlCommand command);
        Task UpdateAsync(RemoteControlCommand command);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<RemoteControlCommand>> GetPendingCommandsByDeviceIdAsync(Guid deviceId);
    }
}
