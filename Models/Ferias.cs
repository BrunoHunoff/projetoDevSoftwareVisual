public class Ferias
{
    public int Id { get; set; }
    
    // início do período de férias
    public DateTime DataInicio { get; set; }

    // fim do período de férias
    public DateTime DataFim { get; set; }

    public int FuncionarioId { get; set; }
    
    public Funcionario? Funcionario { get; set; }  

    public string? Observacoes { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, DataInicio: {DataInicio}, DataFim: {DataFim}, FuncionarioId: {FuncionarioId}";
    }
}