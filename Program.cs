using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDataBase>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "API");

app.MapRotas();
app.MapContratosApi();
app.MapDepartamentoApi();
app.MapBeneficiosApi();
app.MapFeriasApi();
app.MapFuncionarioApi();
app.MapHistoricoSalarioApi();
app.MapPontoApi();

app.Run();
