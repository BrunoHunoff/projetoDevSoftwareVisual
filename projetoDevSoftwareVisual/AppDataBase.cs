using Microsoft.EntityFrameworkCore;

public class AppDataBase : DbContext
{

     public AppDataBase(DbContextOptions<AppDataBase> options)
        : base(options)
    {
    }
    //Configuração da conexão
    protected override void OnConfiguring(
        DbContextOptionsBuilder builder)
    {
        //configuração de acesso ao banco local
        string con = "server=localhost;port=3306;" + 
        "database=RH;user=root;password=arnaldo";

        ////substituir "password" pela sua senha local
        builder.UseMySQL(con)
       .LogTo(Console.WriteLine, LogLevel.Information);
    }

     
    public DbSet<Funcionario> Funcionarios { get; set;}
    public DbSet<Contrato> Contratos { get; set; }

}

