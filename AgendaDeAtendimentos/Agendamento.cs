using Microsoft.VisualBasic;
using System.Security.Policy;
using System.Linq;

public class Agendamento
{
    //Um objeto do tipo Cliente
    // Isso é uma composição: um Agendamento "tem um" Cliente dentro dele
    public Cliente Cliente { get; set; }
    public Servico Servico { get; set; } //Um agendamento tem um sevico dentro dele
    public DateTime DataHora { get; set; } //Data and time é um tipo do C# que vai guardar a DATA e a Hora tipo 22/02/2026 18:50]
    public string Status { get; private set; } //No "private set" só a própria classe pode alterar os status, isso é encapsulamento , proteger o dado e controlar como ele muda 


    public Agendamento(Cliente cliente, Servico servico, DateTime dataHora)
    {
        Cliente = cliente;
        Servico = servico;
        DataHora = dataHora;
        Status = "Agendado";
    }

    //Método responsável por alterar o status com segurança
    // Só aceita os 4 valores- qualquer outro é ignorado
    public void AlterarStatus(string novoStatus)
    {
        
            //Array com os únicos valores permitidos
            string[] statusValidos = { "Agendado", "Confirmado", "Cancelado", "Concluido" };

            //Contains() verifica se o novoStatus está dentro dos válidos
            //Se não estiver, sai do método sem alterar nada
            if (!statusValidos.Contains(novoStatus)) return;
            Status = novoStatus;
           }
    public override string ToString()
    {
        return $"{DataHora:HH:mm} | {Cliente.Nome} | {Servico.Nome} | {Status}";
    }
}