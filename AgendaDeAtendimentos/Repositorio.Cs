// "static" significa que ela não precisa ser instanciada com "new"
public static class Repositorio
{
    // List<Cliente> = uma lista que só aceita objetos do tipo Cliente
    // "= new List<Cliente>()" inicializa a lista vazia na hora que o programa abre
    public static List<Cliente> Clientes { get; } = new List<Cliente> ();

    public static List<Servico> Servicos { get; } = new List<Servico>();
    public static List<Agendamento> Agendamentos { get; } = new List<Agendamento>();
}
