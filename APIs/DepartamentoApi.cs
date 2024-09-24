
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

public static class DepartamentoApi {
    public static async void MapDepartamentoApi(this WebApplication app) {
        var group= app.MapGroup("/");

        //GET

        group.MapGet("/departamentos", async (AppDataBase db) => 
            await db.Departamentos.ToListAsync()
        );

        //GET{ID}

        group.MapGet("/departamentos/{id}", async (AppDataBase db, int id) =>
        
            await db.Departamentos.FindAsync(id) is Departamento departamento
            ? Results.Ok(departamento)
            : Results.NotFound()
        );


        //POST
        group.MapPost("/departamentos", async(AppDataBase db, Departamento departamento) =>
        {
            db.Departamentos.Add(departamento);
            await db.SaveChangesAsync();
            return Results.Created($"/departamentos/{departamento.Id}", departamento);
        });

        //DELETE
        group.MapDelete("/departamentos/{id}", async(AppDataBase db, int id) => {{

            if(await db.Departamentos.FindAsync(id) is Departamento departamento) {
                db.Remove(departamento);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }});

        //PUT
        group.MapPut("/departamentos/{id}", async(AppDataBase db, int id, Departamento departamentoAlterado) =>
        {
            var departamento = await db.Departamentos.FindAsync(id);
            if (departamento is null) return Results.NotFound();

            departamento.NomeDepartamento = departamentoAlterado.NomeDepartamento;
            departamento.Descricao = departamentoAlterado.Descricao;

            await db.SaveChangesAsync();

            return Results.NoContent();   
        });
    }
}