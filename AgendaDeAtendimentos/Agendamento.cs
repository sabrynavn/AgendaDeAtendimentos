// Meu molde pra criar agendamentos
// Um agendamento conecta um cliente, um serviço,
// uma data/hora e um status.

// "using" importa ferramentas de outros lugares do C#
// Microsoft.VisualBasic tem funções auxiliares 
// System.Security.Policy é para segurança , nem usei essa bomba
// System.Linq permite usar .Contains() em listas e arrays
using Microsoft.VisualBasic;
using System.Security.Policy;
using System.Linq;

public class Agendamento
{
    //Propriedades

    // "Composição": um Agendamento TEM UM Cliente dentro dele
    // Em vez de guardar só o nome, guardamos o objeto Cliente inteiro
    // Isso permite acessar cliente.Nome, cliente.Telefone, etc.
    public Cliente Cliente { get; set; }

    // Mesma ideia: um Agendamento TEM UM Servico dentro dele
    public Servico Servico { get; set; }

    // DateTime é um tipo do C# que guarda DATA e HORA juntos
    // Exemplo: 22/02/2026 18:50
    public DateTime DataHora { get; set; }

    // "private set" = só a própria classe pode alterar o status
    // Isso é "encapsulamento" — proteger o dado e controlar como ele muda
    // Para alterar, usamos o método AlterarStatus() lá embaixo
    public string Status { get; private set; }

    // construtor

    // Para criar um agendamento, precisamos de:
    // - Um cliente (objeto Cliente)
    // - Um serviço (objeto Servico)
    // - Uma data/hora (DateTime)
    // Exemplo: new Agendamento(clienteObj, servicoObj, DateTime.Now)
    public Agendamento(Cliente cliente, Servico servico, DateTime dataHora)
    {
        Cliente = cliente;
        Servico = servico;
        DataHora = dataHora;

        // TODO agendamento começa com o status "Agendado"
        Status = "Agendado";
    }

    // Método para alterar o status com segurança

    // Como Status tem "private set", precisamos desse método
    // para alterar o status por fora da classe
    // Ele SÓ aceita os 4 valores válidos — qualquer outro é ignorado
    public void AlterarStatus(string novoStatus)
    {
        // Array com os únicos valores permitidos
        string[] statusValidos = { "Agendado", "Confirmado", "Cancelado", "Concluido" };

        // Contains() verifica se o novoStatus está dentro dos válidos
        // Se não estiver, sai do método sem alterar nada
        if (!statusValidos.Contains(novoStatus)) return;

        // Se passou pela verificação, atualiza o status
        Status = novoStatus;
    }

    // ToString

    // Define como o agendamento aparece na lista
    // ":HH:mm" formata a hora para mostrar só horas e minutos
    // Exemplo: "18:50 | João | Corte | Agendado"
    public override string ToString()
    {
        return $"{DataHora:HH:mm} | {Cliente.Nome} | {Servico.Nome} | {Status}";
    }
}
