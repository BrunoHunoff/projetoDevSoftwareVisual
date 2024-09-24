public class HistoricoSalario
{
    public int Id { get; set; }
    public int FuncionarioId { get; set; }
    public DateTime DataAlteracao { get; set; }
    public decimal SalarioAntigo { get; set; }
    public decimal SalarioNovo { get; set; }
    public string MotivoAlteracao { get; set; }

   
}
