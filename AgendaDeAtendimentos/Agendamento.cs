using Microsoft.VisualBasic;
using System.Security.Policy;

public class Agendamento
{
    //Um objeto do tipo Cliente
    // Isso é uma composição: um Agendamento "tem um" Cliente dentro dele
    public Cliente Cliente { get; set; }
    public Servico Servico {  get; set; } //Um agendamento tem um sevico dentro dele
    public DateTime DataHora { get; set; } //Data and time é um tipo do C# que vai guardar a DATA e a Hora tipo 22/02/2026 18:50]
    public string Status {  get; set; } //O status vai começar como "agendado" mas pode virar "cancelado" ou etc...

    public Servicos(Cliente cliente ,Servico servico, DateTime Datahora )
    {
        Cliente= cliente;
        Servico= servico;
        DateTime = Datahora;

        Status = "Agendado"; //O agendamento nasce com esses status
    }
    public override string ToString()
    {
        // "HH:mm" formata só a hora no padrão 24h  ex: "14:30"
        // Acessa o Nome dentro do objeto Cliente com Cliente.Nome
        // Acessa o Nome dentro do objeto Servico com Servico.Nome
        return $"{DataHora:HH:mm} | {Cliente.Nome} | {Servico.Nome} | {Status}";
  
    }
}
