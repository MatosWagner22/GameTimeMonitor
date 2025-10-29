using AutoMapper;
using GameTimeMonitor.Application.DTOs;
using GameTimeMonitor.Application.Interfaces;
using GameTimeMonitor.Domain.Entities;
using GameTimeMonitor.Domain.Enums;
using GameTimeMonitor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<ActivityDto> GetByIdAsync(Guid id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<IEnumerable<ActivityDto>> GetAllAsync()
        {
            var activities = await _activityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<ActivityDto> CreateAsync(CreateActivityDto createActivityDto)
        {
            // Opcional: Finalizar cualquier actividad activa existente para este dispositivo
            var existingActive = await _activityRepository.GetActiveActivityByDeviceIdAsync(createActivityDto.DeviceId);
            if (existingActive != null)
            {
                existingActive.EndTime = DateTime.UtcNow;
                existingActive.Status = ActivityStatus.Inactive;
                await _activityRepository.UpdateAsync(existingActive);
            }

            var activity = _mapper.Map<Activity>(createActivityDto);
            activity.StartTime = DateTime.UtcNow;
            activity.Status = ActivityStatus.Active;

            var createdActivity = await _activityRepository.AddAsync(activity);
            return _mapper.Map<ActivityDto>(createdActivity);
        }

        public async Task UpdateAsync(Guid id, UpdateActivityDto updateActivityDto)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
            {
                throw new ArgumentException("Activity not found");
            }

            activity.EndTime = updateActivityDto.EndTime ?? DateTime.UtcNow;
            activity.Status = ActivityStatus.Inactive;

            await _activityRepository.UpdateAsync(activity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _activityRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesByDeviceIdAsync(Guid deviceId)
        {
            var activities = await _activityRepository.GetActivitiesByDeviceIdAsync(deviceId);
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<ActivityDto> GetActiveActivityByDeviceIdAsync(Guid deviceId)
        {
            var activity = await _activityRepository.GetActiveActivityByDeviceIdAsync(deviceId);
            return _mapper.Map<ActivityDto>(activity);
        }
    }
}
