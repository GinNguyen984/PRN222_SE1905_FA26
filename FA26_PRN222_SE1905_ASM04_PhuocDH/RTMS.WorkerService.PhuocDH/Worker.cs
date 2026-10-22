using zHoMa.Services.PhuMT;
using System.Text.Json;
using System.Text.Json.Serialization;
using zHoMa.Services.PhuMT.Interface;

namespace RTMS.WorkerService.PhuocDH
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHoursePhuMtService _hoursePhuMtService;

        //public Worker(ILogger<Worker> logger)
        //{
        //    _logger = logger;
        //}

        public Worker(ILogger<Worker> logger, IHoursePhuMtService hoursePhuMtService)
        {
            _logger = logger;
            _hoursePhuMtService = hoursePhuMtService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await this.WriteLogFile();
                }
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task WriteLogFile()
        {
            try
            {
                var items = await _hoursePhuMtService.GetAllAsync();
                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
                var content = JsonSerializer.Serialize(items, opt);
                var filePath = @"D:\BackUp\CN7\PRN222\PhuocDH_SE1905_WS.txt";
                using (var file = File.Open(filePath, FileMode.Append, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        await writer.WriteAsync(string.Format("{0}: {1}", DateTimeOffset.Now.ToString(), content));
                        await writer.FlushAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in worker service: " + ex.Message);
            }
        }
    }
}
