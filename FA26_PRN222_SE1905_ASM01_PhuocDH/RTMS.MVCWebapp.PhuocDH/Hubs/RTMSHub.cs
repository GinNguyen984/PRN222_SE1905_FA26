using Microsoft.AspNetCore.SignalR;
using RTMS.Services.PhuocDH;
namespace RTMS.MVCWebapp.PhuocDH.Hubs
{
    public class RTMSHub : Hub
    {
        private readonly IRacePhuocDhService _racePhuocDhService;
        public RTMSHub(IRacePhuocDhService racePhuocDhService)
        {
            _racePhuocDhService = racePhuocDhService;
        }

        public async Task HubDelete_RacePhuocDh(int id)
        {
            var result = await _racePhuocDhService.DeleteAsync(id);
            if (result)
            {
                await Clients.All.SendAsync("Receiver_Delete_RacePhuocDh", id);
            }
        }
    }
}
