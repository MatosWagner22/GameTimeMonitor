using GameTimeMonitor.Application.DTOs;
using GameTimeMonitor.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameTimeMonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("device/{deviceId}")]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetByDeviceId(Guid deviceId)
        {
            // TODO: Validar que el padre tenga acceso a este deviceId
            var activities = await _activityService.GetActivitiesByDeviceIdAsync(deviceId);
            return Ok(activities);
        }

        [HttpGet("device/{deviceId}/active")]
        public async Task<ActionResult<ActivityDto>> GetActiveByDeviceId(Guid deviceId)
        {
            // TODO: Validar permisos
            var activity = await _activityService.GetActiveActivityByDeviceIdAsync(deviceId);
            if (activity == null)
            {
                return NotFound("No active activity found for this device.");
            }
            return Ok(activity);
        }

        // El agente cliente usará este endpoint para iniciar una actividad
        [HttpPost]
        public async Task<ActionResult<ActivityDto>> Post([FromBody] CreateActivityDto createActivityDto)
        {
            // TODO: El agente cliente deberá autenticarse (quizás con una API Key de dispositivo)
            var activity = await _activityService.CreateAsync(createActivityDto);
            return CreatedAtAction(nameof(GetByDeviceId), new { deviceId = activity.DeviceId }, activity);
        }

        // El agente cliente usará este endpoint para finalizar una actividad
        [HttpPut("{id}/stop")]
        public async Task<IActionResult> Stop(Guid id)
        {
            try
            {
                // TODO: Autenticar al agente
                await _activityService.UpdateAsync(id, new UpdateActivityDto { EndTime = DateTime.UtcNow });
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
