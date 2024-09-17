var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

app.MapGet("/", () => "Hello World!");

app.Run();



app.UseSwagger();
app.UseSwaggerUI();