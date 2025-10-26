using GameTimeMonitor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Interfaces
{
    public interface IDeviceService
    {
        Task<DeviceDto> GetByIdAsync(Guid id);
        Task<IEnumerable<DeviceDto>> GetAllAsync();
        Task<DeviceDto> CreateAsync(CreateDeviceDto createDeviceDto);
        Task UpdateAsync(Guid id, UpdateDeviceDto updateDeviceDto);
        Task DeleteAsync(Guid id);
        Task<DeviceDto> GetByDeviceIdentifierAsync(string deviceIdentifier);
        Task<IEnumerable<DeviceDto>> GetDevicesByUserIdAsync(Guid userId);
    }
}
