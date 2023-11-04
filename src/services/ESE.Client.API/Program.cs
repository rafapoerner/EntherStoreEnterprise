using ESE.Clients.API.Configuration;
using ESE.WebApi.Core.Identity;
using MediatR;
using System.Reflection;

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

builder.Services.AddApiConfig(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.RegisterServices();


var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfig(app.Environment);

app.Run();
