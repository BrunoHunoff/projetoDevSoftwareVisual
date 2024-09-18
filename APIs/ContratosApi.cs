using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlX.XDevAPI.Common;

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
        //UPDATE

        //DELETE
    }
}