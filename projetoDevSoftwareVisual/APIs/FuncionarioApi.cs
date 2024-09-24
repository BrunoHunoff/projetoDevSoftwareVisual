using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            if (!ValidateFuncionario(funcionario, out var errors))
            {
                return Results.BadRequest(errors);
            }

            db.Funcionarios.Add(funcionario);
            await db.SaveChangesAsync();
            return Results.Created($"/funcionario/{funcionario.Id}", funcionario);
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
            if (!ValidateFuncionario(funcionarioAlterado, out var errors))
            {
                return Results.BadRequest(errors);
            }

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

    private static bool ValidateFuncionario(Funcionario funcionario, out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(funcionario.Nome))
            errors.Add("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(funcionario.CPF))
            errors.Add("O CPF é obrigatório.");

        if (funcionario.Salario <= 0)
            errors.Add("O salário deve ser maior que zero.");

        return errors.Count == 0;
    }
}
