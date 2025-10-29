using GameTimeMonitor.Domain.Entities;
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
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;

        public DeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Device> GetByIdAsync(Guid id)
        {
            return await _context.Devices
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _context.Devices
                .Include(d => d.User)
                .ToListAsync();
        }

        public async Task<Device> AddAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task UpdateAsync(Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Device> GetByDeviceIdentifierAsync(string deviceIdentifier)
        {
            return await _context.Devices
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.DeviceIdentifier == deviceIdentifier);
        }

        public async Task<IEnumerable<Device>> GetDevicesByUserIdAsync(Guid userId)
        {
            return await _context.Devices
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }
    }
}
