using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public static class ConfigurarRotasCargo
{
    public static void MapRotas(WebApplication app)
    {
        app.MapGet("/Cargos", async (AppDataBase db) =>
            await db.Cargos.ToListAsync());

        app.MapGet("/Cargos/{id}", async (int id, AppDataBase db) =>
            await db.Cargos.FindAsync(id) is Cargo cargo
                ? Results.Ok(cargo)
                : Results.NotFound());

        app.MapPost("/Cargos", async (Cargo cargo, AppDataBase db) =>
        {
            db.Cargos.Add(cargo);
            await db.SaveChangesAsync();
            return Results.Created($"/Cargos/{cargo.Id}", cargo);
        });

        app.MapPut("/Cargos/{id}", async (int id, Cargo cargoAlterado, AppDataBase db) =>
        {
            var cargo = await db.Cargos.FindAsync(id);
            if (cargo is null) return Results.NotFound();

            cargo.nome = cargoAlterado.nome;
            cargo.descricao = cargoAlterado.descricao;
            cargo.salarioBase = cargoAlterado.salarioBase;
            cargo.departamento = cargoAlterado.departamento;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/Cargos/{id}", async (int id, AppDataBase db) =>
        {
            if (await db.Cargos.FindAsync(id) is Cargo cargo)
            {
                db.Cargos.Remove(cargo);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }
}
