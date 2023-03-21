using Authors.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddHostedService<Messages>();
var app = builder.Build();
app.MapControllers();
app.MapGet("/", () => "Authors");

app.Run();