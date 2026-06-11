
// "Banco de dados" temporário

//
//  simula um banco de dados usando listas na memória.
// Enquanto o programa estiver rodando, os dados ficam guardados aqui.
// Quando o programa fecha, tudo é perdido (porque não salvamos em arquivo).
//
// "static" = não precisa dar "new Repositorio()" para usar
// Todas as propriedades são static, então podemos acessar de qualquer lugar
// Exemplo: Repositorio.Clientes.Add(...)

public static class Repositorio
{
    // List<Cliente> = uma lista que só aceita objetos do tipo Cliente
    // "= new List<Cliente>()" inicializa a lista vazia na hora que o programa abre
    // "get;" significa que qualquer um pode LER a lista
    // (sem "set" porque ninguém pode trocar a lista inteira, só mexer dentro dela)
    public static List<Cliente> Clientes { get; } = new List<Cliente>();

   
    public static List<Servico> Servicos { get; } = new List<Servico>();

    
    public static List<Agendamento> Agendamentos { get; } = new List<Agendamento>();
}
