using GameTimeMonitor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Interfaces
{
    public interface IRemoteControlCommandService
    {
        Task<RemoteControlCommandDto> GetByIdAsync(Guid id);
        Task<IEnumerable<RemoteControlCommandDto>> GetAllAsync();
        Task<RemoteControlCommandDto> CreateAsync(CreateRemoteControlCommandDto createCommandDto);
        Task UpdateAsync(Guid id, RemoteControlCommandDto updateCommandDto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<RemoteControlCommandDto>> GetPendingCommandsByDeviceIdAsync(Guid deviceId);
    }
}
