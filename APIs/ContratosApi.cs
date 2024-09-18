
using Microsoft.EntityFrameworkCore;

public static class ContratosApi {
    public static void MapAlunosApi(this WebApplication app) {
        var group= app.MapGroup("/contratos");

        //GET

        group.MapGet("/", async (AppDataBase db) => {
            await db.Contratos.ToListAsync();
        });

        //GET{ID}

        group.MapGet("/{id}", async (AppDataBase db, int id) =>
            await db.Contratos.FindAsync(id) is Contrato contrato
            ? Results.Ok(contrato)
            : Results.NotFound());


        //POST
        group.MapPost("/", async (AppDataBase db, Contrato contrato) => {
            db.Contratos.Add(contrato);
            await db.SaveChangesAsync();
            return Results.Created($"/contratos/{contrato.Id}", contrato);
        });

        //DELETE
        group.MapDelete("/{id}", async (AppDataBase db, int id) => {{
            if(await db.Contratos.FindAsync(id) is Contrato contrato) {
                db.Remove(contrato);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }});

        //UPDATE
        group.MapPut("/{id}", async (AppDataBase db,int id, Contrato contratoAlterado) => {
            var contrato = await db.Contratos.FindAsync(id);

            if(contratoAlterado == null) return Results.NotFound();

            contrato.Beneficios = contratoAlterado.Beneficios;
            contrato.DataInicio = contratoAlterado.DataInicio;
            contrato.DataFim = contratoAlterado.DataFim;
            contrato.Observacoes = contratoAlterado.Observacoes;
            contrato.Beneficios = contratoAlterado.Beneficios;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}