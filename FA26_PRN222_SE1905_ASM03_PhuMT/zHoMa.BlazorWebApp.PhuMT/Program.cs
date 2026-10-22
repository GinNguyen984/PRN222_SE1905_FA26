using zHoMa.BlazorWebApp.PhuMT.Components;
using Microsoft.EntityFrameworkCore;
using zHoMa.Repositories.PhuMT;
using zHoMa.Services.PhuMT.Interface;
using zHoMa.Services.PhuMT.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<RTMSContext>();
builder.Services.AddScoped<IHoursePhuMtService, HoursePhuMtService>();
builder.Services.AddScoped<IStablePhuMtService, StablePhuMtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
