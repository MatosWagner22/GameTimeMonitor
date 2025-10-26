using GameTimeMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Domain.Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device> GetByIdAsync(Guid id);
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device> AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task DeleteAsync(Guid id);
        Task<Device> GetByDeviceIdentifierAsync(string deviceIdentifier);
        Task<IEnumerable<Device>> GetDevicesByUserIdAsync(Guid userId);
    }
}
