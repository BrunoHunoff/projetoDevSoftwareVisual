public class Contrato {
    public int Id {get; set;}
    public String TipoContrato {get; set;}
    public DateTime DataInicio {get; set;}

    public DateTime DataFim {get; set;}

    public String? Observacoes {get; set;}

    public List<Beneficios>? Beneficios {get; set;}

    public override string ToString(){
        return 
        $"Id: {Id}, TipoContrato: {TipoContrato}, DataInicio: {DataInicio}, DataFim: {DataFim}, Observacoes: {Observacoes}, Beneficios: {Beneficios}";
    }

}