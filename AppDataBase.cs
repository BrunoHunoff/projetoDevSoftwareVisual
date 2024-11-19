using Microsoft.EntityFrameworkCore;

public class AppDataBase : DbContext
{

    //Configuração da conexão
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        //configuração de acesso ao banco local

        ////substituir "password" pela sua senha local
        builder.UseMySQL("server=localhost;port=3306;database=svApi;user=root;password=marcelha");

    }

    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Beneficios> Beneficios { get; set; }
    public DbSet<Departamento> Departamentos{ get; set; }
    public DbSet<Ferias> Ferias {get; set;}
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<HistoricoSalario> HistoricoSalarios {get; set;}
    public DbSet<Ponto> Pontos {get; set;}

}

