using GameTimeMonitor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Interfaces
{
    public interface IActivityService
    {
        Task<ActivityDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ActivityDto>> GetAllAsync();
        Task<ActivityDto> CreateAsync(CreateActivityDto createActivityDto);
        Task UpdateAsync(Guid id, UpdateActivityDto updateActivityDto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ActivityDto>> GetActivitiesByDeviceIdAsync(Guid deviceId);
        Task<ActivityDto> GetActiveActivityByDeviceIdAsync(Guid deviceId);
    }
}
