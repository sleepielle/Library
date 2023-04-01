using Books.Services;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHostedService<Messages>();
builder.Services.AddScoped<Messages>();
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Books");
app.Run();