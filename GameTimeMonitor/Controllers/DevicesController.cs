using GameTimeMonitor.Application.DTOs;
using GameTimeMonitor.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameTimeMonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // Protegeremos esto más adelante
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> Get()
        {
            var devices = await _deviceService.GetAllAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDto>> Get(Guid id)
        {
            var device = await _deviceService.GetByIdAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            return Ok(device);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetByUserId(Guid userId)
        {
            // TODO: Validar que el usuario autenticado (Padre) sea el dueño de este userId (Hijo)
            var devices = await _deviceService.GetDevicesByUserIdAsync(userId);
            return Ok(devices);
        }

        [HttpPost]
        public async Task<ActionResult<DeviceDto>> Post([FromBody] CreateDeviceDto createDeviceDto)
        {
            // TODO: Validar que el usuario autenticado sea el padre del userId en el DTO
            var device = await _deviceService.CreateAsync(createDeviceDto);
            return CreatedAtAction(nameof(Get), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateDeviceDto updateDeviceDto)
        {
            try
            {
                // TODO: Validar permisos
                await _deviceService.UpdateAsync(id, updateDeviceDto);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // TODO: Validar permisos
            await _deviceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
