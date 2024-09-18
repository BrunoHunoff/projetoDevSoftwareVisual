public class Contrato {
    private int Id {get; set;}
    private String TipoContrato {get; set;}
    private DateTime DataInicio {get; set;}

    private DateTime DataFim {get; set;}

    private String? Observacoes {get; set;}

    private List<Beneficios>? Beneficios {get; set;}

    public override string ToString(){
        return 
        $"Id: {Id}, TipoContrato: {TipoContrato}, DataInicio: {DataInicio}, DataFim: {DataFim}, Observacoes: {Observacoes}, Beneficios: {Beneficios}";
    }

}