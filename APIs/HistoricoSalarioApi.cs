using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

public static class HistoricoSalarioApi {

    public static void  MapHistoricoSalarioApi(this WebApplication app){
        var group = app.MapGroup("/historico-salario");

        //Lista todas os salarios
        group.MapGet("/", async (AppDataBase db) =>
            await db.HistoricoSalarios.ToListAsync()
        );

        // Busca salarios por id
        group.MapGet("/{id}", async (AppDataBase db, int id) =>
            await db.HistoricoSalarios.FindAsync(id) is HistoricoSalario historicosalario
            ? Results.Ok(historicosalario)
            : Results.NotFound());

        // Alterar Salario
        group.MapPost("/", async (AppDataBase db, [FromBody] HistoricoSalario historicosalario) =>
        {
            db.HistoricoSalarios.Add(historicosalario);
            await db.SaveChangesAsync();
            return Results.Created($"/historico-salario/{historicosalario.Id}", historicosalario);
        });

        //Remove um registro de salario
        group.MapDelete("/{id}", async (AppDataBase db, int id) =>
        {
            if (await db.HistoricoSalarios.FindAsync(id) is HistoricoSalario historicosalario)
            {
                db.HistoricoSalarios.Remove(historicosalario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });

        //Atualiza um registro de salario
        group.MapPut("/{id}", async (AppDataBase db, int id, HistoricoSalario SalarioNovo) =>
        {
            var historicosalario = await db.HistoricoSalarios.FindAsync(id);

            if (historicosalario == null) return Results.NotFound();

            historicosalario.DataAlteracao = SalarioNovo.DataAlteracao;
            historicosalario.SalarioNovo = SalarioNovo.SalarioNovo;
            historicosalario.FuncionarioId = SalarioNovo.FuncionarioId;
            historicosalario.MotivoAlteracao = SalarioNovo.MotivoAlteracao;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }



}
    