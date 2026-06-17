using System.Collections.Generic;

namespace AgendaDeAtendimentos
{
    // Repositório em memória — mantém os dados enquanto o programa está aberto
    public static class Repositorio
    {
        public static List<Cliente> Clientes = new List<Cliente>();
        public static List<Servico> Servicos = new List<Servico>();
        public static List<Agendamento> Agendamentos = new List<Agendamento>();
    }
}