using AutoMapper;
using GameTimeMonitor.Application.DTOs;
using GameTimeMonitor.Application.Interfaces;
using GameTimeMonitor.Domain.Entities;
using GameTimeMonitor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        public async Task<DeviceDto> GetByIdAsync(Guid id)
        {
            var device = await _deviceRepository.GetByIdAsync(id);
            return _mapper.Map<DeviceDto>(device);
        }

        public async Task<IEnumerable<DeviceDto>> GetAllAsync()
        {
            var devices = await _deviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DeviceDto>>(devices);
        }

        public async Task<DeviceDto> CreateAsync(CreateDeviceDto createDeviceDto)
        {
            var device = _mapper.Map<Device>(createDeviceDto);
            device.Status = Domain.Enums.DeviceStatus.Offline; // Default status
            var createdDevice = await _deviceRepository.AddAsync(device);
            return _mapper.Map<DeviceDto>(createdDevice);
        }

        public async Task UpdateAsync(Guid id, UpdateDeviceDto updateDeviceDto)
        {
            var device = await _deviceRepository.GetByIdAsync(id);
            if (device == null)
            {
                throw new ArgumentException("Device not found");
            }

            _mapper.Map(updateDeviceDto, device);
            await _deviceRepository.UpdateAsync(device);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _deviceRepository.DeleteAsync(id);
        }

        public async Task<DeviceDto> GetByDeviceIdentifierAsync(string deviceIdentifier)
        {
            var device = await _deviceRepository.GetByDeviceIdentifierAsync(deviceIdentifier);
            return _mapper.Map<DeviceDto>(device);
        }

        public async Task<IEnumerable<DeviceDto>> GetDevicesByUserIdAsync(Guid userId)
        {
            var devices = await _deviceRepository.GetDevicesByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<DeviceDto>>(devices);
        }
    }
}
