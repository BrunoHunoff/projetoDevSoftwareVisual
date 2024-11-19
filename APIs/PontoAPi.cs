using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

public static class PontoApi {
    public static async void MapPontoApi(this WebApplication app) {
        var group= app.MapGroup("/");

        //GET

        group.MapGet("/pontos", async (AppDataBase db) => 
            await db.Pontos.ToListAsync()
        );

        //GET{ID}

        group.MapGet("/pontos/{id}", async (AppDataBase db, int id) =>
        
            await db.Pontos.FindAsync(id) is Ponto ponto
            ? Results.Ok(ponto)
            : Results.NotFound()
        );


        //POST
        group.MapPost("/pontos", async(AppDataBase db, Ponto ponto) =>
        {
            db.Pontos.Add(ponto);
            await db.SaveChangesAsync();
            return Results.Created($"/pontos/{ponto.Id}", ponto);
        });

        //DELETE
        group.MapDelete("/pontos/{id}", async(AppDataBase db, int id) => {{

            if(await db.Pontos.FindAsync(id) is Ponto ponto) {
                db.Remove(ponto);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }});

        //PUT
        group.MapPut("/pontos/{id}", async(AppDataBase db, int id, Ponto pontoAlterado) =>
        {
            var ponto = await db.Pontos.FindAsync(id);
            if (ponto is null) return Results.NotFound();

            ponto.NomeFuncionario = pontoAlterado.NomeFuncionario;
            ponto.HoraEntrada = pontoAlterado.HoraEntrada;
            ponto.HoraSaida = pontoAlterado.HoraSaida;

            await db.SaveChangesAsync();

            return Results.NoContent();   
        });
    }
}