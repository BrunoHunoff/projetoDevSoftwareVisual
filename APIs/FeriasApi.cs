using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class FeriasApi
{
    public static void MapFeriasApi(this WebApplication app)
    {
        var group = app.MapGroup("/ferias");

        //Lista todas as férias
        group.MapGet("/", async (AppDataBase db) =>
            await db.Ferias.ToListAsync()
        );

        // Busca férias por ID
        group.MapGet("/{id}", async (AppDataBase db, int id) =>
            await db.Ferias.FindAsync(id) is Ferias ferias
            ? Results.Ok(ferias)
            : Results.NotFound());

        //Cria um novo registro de férias
        group.MapPost("/", async (AppDataBase db, [FromBody] Ferias ferias) =>
        {
            db.Ferias.Add(ferias);
            await db.SaveChangesAsync();
            return Results.Created($"/ferias/{ferias.Id}", ferias);
        });

        //Remove um registro de férias
        group.MapDelete("/{id}", async (AppDataBase db, int id) =>
        {
            if (await db.Ferias.FindAsync(id) is Ferias ferias)
            {
                db.Ferias.Remove(ferias);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });

        //Atualiza um registro de férias
        group.MapPut("/{id}", async (AppDataBase db, int id, Ferias feriasAlterado) =>
        {
            var ferias = await db.Ferias.FindAsync(id);

            if (ferias == null) return Results.NotFound();

            ferias.DataInicio = feriasAlterado.DataInicio;
            ferias.DataFim = feriasAlterado.DataFim;
            ferias.FuncionarioId = feriasAlterado.FuncionarioId;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}