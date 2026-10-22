using Microsoft.AspNetCore.SignalR;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.MVCWebApp.PhuMT.Hubs
{
    public class RTMSHub : Hub
    {
        private readonly IHoursePhuMtService _hoursePhuMtService;

        public RTMSHub(IHoursePhuMtService hoursePhuMtService) => _hoursePhuMtService = hoursePhuMtService;
            
        public async Task<bool> HubDelete_IHoursePhuMt(int id)
        {
            try
            {
                var result = await _hoursePhuMtService.Delete(id);
                if (result)
                {
                    await Clients.All.SendAsync("Receiver_Delete", id);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RTMSHub] Error deleting horse #{id}: {ex.Message}");
                throw;
            }
            return false;
        }
    }
}
