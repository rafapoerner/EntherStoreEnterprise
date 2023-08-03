using ESE.Identity.API.Configuration;
using ESE.WebApp.MVC.Configuration;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
builder.Services.AddIdentityConfig();

builder.Services.AddMvcConfig(builder.Configuration);

builder.Services.RegisterServices();

var app = builder.Build();


app.MapControllerRoute(
      name: "default",
      pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.UseMvcConfig(app.Environment);

app.Run();
