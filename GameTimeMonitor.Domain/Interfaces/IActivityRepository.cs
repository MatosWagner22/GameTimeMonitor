using GameTimeMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Domain.Interfaces
{
    public interface IActivityRepository
    {
        Task<Activity> GetByIdAsync(Guid id);
        Task<IEnumerable<Activity>> GetAllAsync();
        Task<Activity> AddAsync(Activity activity);
        Task UpdateAsync(Activity activity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Activity>> GetActivitiesByDeviceIdAsync(Guid deviceId);
        Task<Activity> GetActiveActivityByDeviceIdAsync(Guid deviceId);
    }
}
