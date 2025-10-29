using GameTimeMonitor.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace GameTimeMonitor.Application.Hubs
{
    [Authorize] // Solo usuarios autenticados pueden conectarse al hub
    public class ControlHub : Hub
    {
        // El agente cliente (Hijo) llamará a esto al conectarse
        public async Task JoinDeviceGroup(string deviceIdentifier)
        {
            // El groupName será el identificador del dispositivo (p.ej. MAC Address)
            // o el DeviceId (Guid)
            await Groups.AddToGroupAsync(Context.ConnectionId, deviceIdentifier);
            // Podríamos notificar al padre que el dispositivo está online
            // ...
        }

        // El Padre (desde la app web/móvil) NO llamará a esto directamente.
        // El Controlador API llamará a esto.
        public async Task SendCommandToDevice(string deviceIdentifier, CommandType command, string commandId)
        {
            // Enviar el comando al grupo específico (el dispositivo que escucha)
            await Clients.Group(deviceIdentifier).SendAsync("ReceiveCommand", command.ToString(), commandId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Aquí podríamos actualizar el estado del dispositivo a 'Offline'
            // Necesitaríamos una forma de mapear ConnectionId a DeviceId
            await base.OnDisconnectedAsync(exception);
        }
    }
}
