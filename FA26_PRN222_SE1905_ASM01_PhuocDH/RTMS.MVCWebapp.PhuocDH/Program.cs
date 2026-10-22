using Microsoft.AspNetCore.Authentication.Cookies;
using RTMS.Services.PhuocDH;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRacePhuocDhService, RacePhuocDhService>();
builder.Services.AddScoped<IRaceResultPhuocDhService, RaceResultPhuocDhService>();
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

app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapHub<RTMS.MVCWebapp.PhuocDH.Hubs.RTMSHub>("/rthub");
app.Run();
