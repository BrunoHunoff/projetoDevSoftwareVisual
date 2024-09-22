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
    public DbSet<Departamento> Departamentos{ get; set; }
    

}
