using RTMS.WorkerService.PhuocDH;
using zHoMa.Services.PhuMT.Interface;   
using zHoMa.Services.PhuMT.Service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "RTMS.WorkerService.PhuocDH";
});

builder.Services.AddSingleton<IHoursePhuMtService, HoursePhuMtService>();

var host = builder.Build();
host.Run();
