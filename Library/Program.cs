using Books.Services;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<IAuthors, Authors>();
builder.Services.AddScoped<sendMessage>();
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Books");
app.Run();