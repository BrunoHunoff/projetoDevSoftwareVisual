using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class FuncionarioApi
{
    public static void MapFuncionarioApi(WebApplication app)
    {
       

        
        app.MapGet("/Funcionario", async (AppDataBase db) => 
            await db.Funcionarios.ToListAsync()
        );

        
        app.MapGet("/Funcionario/{id}", async (int id, AppDataBase db) =>
            await db.Funcionarios.FindAsync(id) is Funcionario funcionario
            ? Results.Ok(funcionario)
            : Results.NotFound()
        );

        
        app.MapPost("/Funcionario", async (AppDataBase db, [FromBody] Funcionario funcionario) =>
        {
            db.Funcionarios.Add(funcionario);
            await db.SaveChangesAsync();
            return Results.Created($"/funcionarios/{funcionario.Id}", funcionario);
        });

        
        app.MapDelete("/Funcionario/{id}", async (AppDataBase db, int id) =>
        {
            if (await db.Funcionarios.FindAsync(id) is Funcionario funcionario)
            {
                db.Funcionarios.Remove(funcionario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });

        
        app.MapPut("/Funcionario/{id}", async (AppDataBase db, int id, Funcionario funcionarioAlterado) =>
        {
            var funcionario = await db.Funcionarios.FindAsync(id);
            if (funcionario == null) return Results.NotFound();

            funcionario.Nome = funcionarioAlterado.Nome;
            funcionario.CPF = funcionarioAlterado.CPF;
            funcionario.Cargo = funcionarioAlterado.Cargo;
            funcionario.DataContratacao = funcionarioAlterado.DataContratacao;
            funcionario.Salario = funcionarioAlterado.Salario;
            funcionario.Endereco = funcionarioAlterado.Endereco;
            funcionario.Telefone = funcionarioAlterado.Telefone;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
