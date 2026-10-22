using Microsoft.AspNetCore.Authentication.Cookies;
using zHoMa.MVCWebApp.PhuMT.Hubs;
using zHoMa.Repositories.PhuMT;
using zHoMa.Services.PhuMT.Interface;
using zHoMa.Services.PhuMT.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RTMSContext>();

builder.Services.AddScoped<IHoursePhuMtService, HoursePhuMtService>();
builder.Services.AddScoped<IStablePhuMtService, StablePhuMtService>();
builder.Services.AddScoped<ISystemUserAccountPhuMtService, SystemUserAccountService>();
builder.Services.AddScoped<SystemUserAccountService>();
builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Forbidden");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    });
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<RTMSHub>("/RTMSHub");
app.Run();
