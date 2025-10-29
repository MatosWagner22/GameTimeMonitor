using AutoMapper;
using GameTimeMonitor.Application.DTOs;
using GameTimeMonitor.Application.Hubs;
using GameTimeMonitor.Application.Interfaces;
using GameTimeMonitor.Domain.Entities;
using GameTimeMonitor.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameTimeMonitor.Application.Services
{
    public class RemoteControlCommandService : IRemoteControlCommandService
    {
        private readonly IRemoteControlCommandRepository _commandRepository;
        private readonly IDeviceRepository _deviceRepository; // Necesitamos esto
        private readonly IMapper _mapper;
        private readonly IHubContext<ControlHub> _hubContext; // <-- Inyectar Hub

        public RemoteControlCommandService(
            IRemoteControlCommandRepository commandRepository,
            IDeviceRepository deviceRepository, // <-- Añadir
            IMapper mapper,
            IHubContext<ControlHub> hubContext) // <-- Inyectar
        {
            _commandRepository = commandRepository;
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<RemoteControlCommandDto> GetByIdAsync(Guid id)
        {
            var command = await _commandRepository.GetByIdAsync(id);
            return _mapper.Map<RemoteControlCommandDto>(command);
        }

        public async Task<IEnumerable<RemoteControlCommandDto>> GetAllAsync()
        {
            var commands = await _commandRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RemoteControlCommandDto>>(commands);
        }

        public async Task<RemoteControlCommandDto> CreateAsync(CreateRemoteControlCommandDto createCommandDto)
        {
            var command = _mapper.Map<RemoteControlCommand>(createCommandDto);
            command.IssuedAt = DateTime.UtcNow;
            command.Executed = false;

            var createdCommand = await _commandRepository.AddAsync(command);

            // Obtener el identificador del dispositivo
            var device = await _deviceRepository.GetByIdAsync(command.DeviceId);
            if (device != null)
            {
                // Enviar comando vía SignalR al grupo (dispositivo) específico
                await _hubContext.Clients.Group(device.DeviceIdentifier)
                    .SendAsync("ReceiveCommand", command.Command.ToString(), command.Id.ToString());
            }

            return _mapper.Map<RemoteControlCommandDto>(createdCommand);
        }

        public async Task UpdateAsync(Guid id, RemoteControlCommandDto updateCommandDto)
        {
            var command = await _commandRepository.GetByIdAsync(id);
            if (command == null)
            {
                throw new ArgumentException("Command not found");
            }

            // Solo permitimos actualizar el estado de 'Executed'
            command.Executed = updateCommandDto.Executed;

            await _commandRepository.UpdateAsync(command);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _commandRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RemoteControlCommandDto>> GetPendingCommandsByDeviceIdAsync(Guid deviceId)
        {
            var commands = await _commandRepository.GetPendingCommandsByDeviceIdAsync(deviceId);
            return _mapper.Map<IEnumerable<RemoteControlCommandDto>>(commands);
        }
    }
}
