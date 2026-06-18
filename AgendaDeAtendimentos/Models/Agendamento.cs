

using System; // Para usar DateTime (data e hora) e Array (para listas)
using AgendaDeAtendimentos.Models;

// Tudo isso já está dentro do namespace Models, mas o using é necessário
// por segurança para garantir que as classes Cliente e Servico sejam encontradas.
namespace AgendaDeAtendimentos.Models
{
    // Classe que representa um agendamento.
    public class Agendamento
    {
        // --- PROPRIEDADES ---

        // Id: identificador único do agendamento no banco.
        public int Id { get; set; }

        // ClienteId: o ID do cliente que agendou.
        // É uma "chave estrangeira" - aponta para o ID do cliente na tabela clientes.
        // Guardamos só o número (ID) em vez do objeto inteiro para o banco funcionar.
        public int ClienteId { get; set; }

        // ServicoId: o ID do serviço que foi agendado.
        // Mesma lógica - guardamos o ID para o banco.
        public int ServicoId { get; set; }

        // DataHora: o dia e a hora marcados.
        // DateTime guarda data E hora juntos (ex: 25/12/2025 14:30:00).
        public DateTime DataHora { get; set; }

        // Status: a situação do agendamento.
        // "= \"Agendado\"" = quando cria, já começa como "Agendado".
        public string Status { get; set; } = "Agendado";

        // Observacao: um campo extra para anotações (ex: "cliente prefere tal produto").
        public string Observacao { get; set; } = "";

        public Cliente? Cliente { get; set; }

        // Servico? - mesma coisa, para exibir o nome do serviço na lista.
        public Servico? Servico { get; set; }


        // --- CONSTRUTORES ---

        // Construtor vazio - cria um agendamento sem dados (raro, mas útil).
        public Agendamento() { }

        // Construtor que recebe o cliente, o serviço e a data/hora.
        // Esse é usado quando a pessoa clica em "Agendar" na tela.
        // Repare que recebemos os OBJETOS (Cliente e Servico) completos,
        // mas extraímos só os IDs para guardar no banco.
        public Agendamento(Cliente cliente, Servico servico, DateTime dataHora)
        {
            // Pega o ID do objeto Cliente e guarda em ClienteId (int simples).
            ClienteId = cliente.Id;
            // Pega o ID do objeto Servico e guarda em ServicoId.
            ServicoId = servico.Id;
            // Guarda a data/hora recebida.
            DataHora = dataHora;
            // Todo agendamento novo começa como "Agendado".
            Status = "Agendado";
            // Guarda os objetos também para exibir na tela sem precisar buscar de novo.
            Cliente = cliente;
            Servico = servico;
        }


        // --- MÉTODOS ---

        // AlterarStatus: muda o status do agendamento.
        // Só aceita status válidos (não pode inventar um status qualquer).
        public void AlterarStatus(string novoStatus)
        {
            // Lista de status que são permitidos no sistema.
            string[] validos = { "Agendado", "Confirmado", "Cancelado", "Concluido" };

            // Array.IndexOf procura o novoStatus dentro da lista validos.
            // Se encontrar, retorna a posição (0, 1, 2 ou 3).
            // Se não encontrar, retorna -1.
            // Se >= 0, significa que encontrou, então pode mudar.
            if (Array.IndexOf(validos, novoStatus) >= 0)
                Status = novoStatus;
            // Se não for válido, simplesmente não faz nada (ignora).
        }
        public override string ToString() =>
            $"{DataHora:HH:mm} | {Cliente?.Nome} | {Servico?.Nome} | {Status}";
    }
}
