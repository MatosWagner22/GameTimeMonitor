using GameTimeMonitor.Domain.Entities;
using GameTimeMonitor.Domain.Enums;
using GameTimeMonitor.Domain.Interfaces;
using GameTimeMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Infrastructure.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Activity> GetByIdAsync(Guid id)
        {
            return await _context.Activities
                .Include(a => a.Device)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _context.Activities
                .Include(a => a.Device)
                .ToListAsync();
        }

        public async Task<Activity> AddAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task UpdateAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByDeviceIdAsync(Guid deviceId)
        {
            return await _context.Activities
                .Where(a => a.DeviceId == deviceId)
                .OrderByDescending(a => a.StartTime)
                .ToListAsync();
        }

        public async Task<Activity> GetActiveActivityByDeviceIdAsync(Guid deviceId)
        {
            return await _context.Activities
                .Where(a => a.DeviceId == deviceId && a.Status == ActivityStatus.Active)
                .FirstOrDefaultAsync();
        }
    }
}
