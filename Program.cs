using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDataBase>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "API");

app.MapRotas();
app.MapContratosApi();
app.MapDepartamentoApi();
app.MapBeneficiosApi();
app.MapFeriasApi();

app.Run();