using kinder_care.Models;
using kinder_care.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Replace the connection string placeholder with the environment variable
var connectionString = builder.Configuration.GetConnectionString("KinderCareConnection")
    ?.Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "");

if (builder.Environment.IsProduction() && string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_PASSWORD")))
{
    throw new InvalidOperationException("The DB_PASSWORD environment variable is not set.");
}

builder.Services.AddDbContext<KinderCareContext>(options =>
    options.UseSqlServer(connectionString));

// Register ExpedienteService
builder.Services.AddScoped<ExpedienteService>();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Access/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});

builder.WebHost.UseUrls("http://0.0.0.0:80");

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();