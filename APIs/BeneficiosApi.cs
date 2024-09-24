using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class BeneficiosApi
{
    public static void MapBeneficiosApi(this WebApplication app)
    {
        var group = app.MapGroup("/beneficios");

        //GET

        group.MapGet("/", async (AppDataBase db) =>
            await db.Beneficios.ToListAsync()
        );

        //GET{ID}

        group.MapGet("/{id}", async (AppDataBase db, int id) =>
            await db.Beneficios.FindAsync(id) is Beneficios beneficio
            ? Results.Ok(beneficio)
            : Results.NotFound());


        //POST
        group.MapPost("/", async (AppDataBase db, [FromBody] Beneficios beneficio) =>
        {
            db.Beneficios.Add(beneficio);
            await db.SaveChangesAsync();
            return Results.Created($"/beneficios/{beneficio.Id}", beneficio);
        });

        //DELETE
        group.MapDelete("/{id}", async (AppDataBase db, int id) =>
        {
            {
                if (await db.Beneficios.FindAsync(id) is Beneficios beneficio)
                {
                    db.Remove(beneficio);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                return Results.NotFound();
            }
        });

        //UPDATE
        group.MapPut("/{id}", async (AppDataBase db, int id, Beneficios beneficioAlterado) =>
        {
            var beneficio = await db.Beneficios.FindAsync(id);

            if (beneficio == null) return Results.NotFound();

            beneficio.NomeBeneficio = beneficioAlterado.NomeBeneficio;
            beneficio.Descricao = beneficioAlterado.Descricao;
            beneficio.Valor = beneficioAlterado.Valor;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}