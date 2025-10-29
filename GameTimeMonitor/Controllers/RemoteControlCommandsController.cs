using GameTimeMonitor.Application.DTOs;
using GameTimeMonitor.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameTimeMonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = nameof(UserRole.Parent))] // Solo los padres pueden enviar comandos
    public class RemoteControlCommandsController : ControllerBase
    {
        private readonly IRemoteControlCommandService _commandService;

        public RemoteControlCommandsController(IRemoteControlCommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpPost]
        public async Task<ActionResult<RemoteControlCommandDto>> Post([FromBody] CreateRemoteControlCommandDto createCommandDto)
        {
            // TODO: Validar que el padre autenticado sea el dueño del dispositivo
            var command = await _commandService.CreateAsync(createCommandDto);

            // Aquí es donde notificaremos al Hub de SignalR
            // _hubContext.Clients.Group(createCommandDto.DeviceId.ToString()).SendAsync("ReceiveCommand", command);

            return CreatedAtAction(nameof(GetById), new { id = command.Id }, command);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RemoteControlCommandDto>> GetById(Guid id)
        {
            // TODO: Validar permisos
            var command = await _commandService.GetByIdAsync(id);
            if (command == null)
            {
                return NotFound();
            }
            return Ok(command);
        }

        [HttpGet("device/{deviceId}/pending")]
        public async Task<ActionResult<RemoteControlCommandDto>> GetPending(Guid deviceId)
        {
            // TODO: Validar permisos (el agente cliente usará esto si SignalR falla)
            var commands = await _commandService.GetPendingCommandsByDeviceIdAsync(deviceId);
            return Ok(commands);
        }

        [HttpPut("{id}/executed")]
        public async Task<IActionResult> MarkAsExecuted(Guid id)
        {
            // TODO: El agente cliente usará esto para marcar un comando como completado
            try
            {
                await _commandService.UpdateAsync(id, new RemoteControlCommandDto { Executed = true });
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
