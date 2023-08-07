using ESE.Identity.API.Configuration;
using ESE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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

app.UseIdentityConfig();

app.UseMvcConfig(app.Environment);

app.Run();
