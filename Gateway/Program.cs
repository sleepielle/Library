
using Gateway.Controllers;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<receiveMessages>();
builder.Services.AddSingleton<IFiles, Files>();
builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
