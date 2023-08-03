using ESE.Identity.API.Configuration;
using ESE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfig();

builder.Services.AddMvcConfig();

builder.Services.RegisterServices();

var app = builder.Build();


app.MapControllerRoute(
      name: "default",
      pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.UseMvcConfig(app.Environment);

app.Run();
