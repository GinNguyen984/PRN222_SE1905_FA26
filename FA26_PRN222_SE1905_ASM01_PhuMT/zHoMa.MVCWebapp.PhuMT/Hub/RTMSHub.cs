using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using zHoMa.Services.PhuMT.Interface;
namespace zHoMa.MVCWebApp.PhuMT.Hubs

{
    public class RTMSHub : Hub
    {
        private readonly IHoursePhuMtService _hoursePhuMtService;
        public RTMSHub(IHoursePhuMtService hoursePhuMtService)  =>   _hoursePhuMtService = hoursePhuMtService;
            
        public async Task HubDelete_IHoursePhuMt(int id)
        {
            var result = await _hoursePhuMtService.Delete(id);
            if (result)
            {
                await Clients.All.SendAsync("Receiver_Delete", id);
            }
        }
    }
}
