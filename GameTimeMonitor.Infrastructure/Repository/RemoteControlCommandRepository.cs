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
    public class RemoteControlCommandRepository : IRemoteControlCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public RemoteControlCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RemoteControlCommand> GetByIdAsync(Guid id)
        {
            return await _context.RemoteControlCommands
                .Include(rc => rc.Device)
                .FirstOrDefaultAsync(rc => rc.Id == id);
        }

        public async Task<IEnumerable<RemoteControlCommand>> GetAllAsync()
        {
            return await _context.RemoteControlCommands
                .Include(rc => rc.Device)
                .ToListAsync();
        }

        public async Task<RemoteControlCommand> AddAsync(RemoteControlCommand command)
        {
            _context.RemoteControlCommands.Add(command);
            await _context.SaveChangesAsync();
            return command;
        }

        public async Task UpdateAsync(RemoteControlCommand command)
        {
            _context.RemoteControlCommands.Update(command);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var command = await _context.RemoteControlCommands.FindAsync(id);
            if (command != null)
            {
                _context.RemoteControlCommands.Remove(command);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RemoteControlCommand>> GetPendingCommandsByDeviceIdAsync(Guid deviceId)
        {
            return await _context.RemoteControlCommands
                .Where(rc => rc.DeviceId == deviceId && !rc.Executed)
                .OrderBy(rc => rc.IssuedAt)
                .ToListAsync();
        }
    }
}
