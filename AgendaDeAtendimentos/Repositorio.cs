// ============================================================
// Repositorio.cs (NÃO ESTAMOS MAIS USANDO PARA SALVAR DADOS)
// ============================================================
//
// ATENÇÃO - LEIA ISSO:
//
// Esse arquivo ainda existe, mas NÃO é mais usado para guardar dados.
// Antes ele era a "memória" do sistema - guardava listas de clientes,
// serviços e agendamentos enquanto o programa estava aberto.
//
// O PROBLEMA:
// Isso era VOLÁTIL - fechou o programa, PERDEU TUDO.
// O briefing pedia: "Os dados sobrevivem ao fechar o programa".
//
// A SOLUÇÃO:
// Agora os dados são salvos no SQLite (arquivo sistema.db).
// O FormPrincipal usa os Services, que usam os Repositories, que salvam no banco.
//
// Esse arquivo ficou aqui só para não dar erro de compilação nos arquivos
// que ainda tinham "using AgendaDeAtendimentos;" e esperavam encontrar a classe.
// Pode ser removido numa limpeza futura.

using System.Collections.Generic;
// Precisa desse using para encontrar as classes Cliente, Servico, Agendamento
// que estão no namespace AgendaDeAtendimentos.Models
using AgendaDeAtendimentos.Models;

namespace AgendaDeAtendimentos
{
    // "static" = não precisa dar "new" para usar - mas por isso mesmo é limitado.
    public static class Repositorio
    {
        // Essas listas existem mas NÃO são mais populadas nem usadas pelo sistema.
        // Se você abrir o programa e cadastrar um cliente, ele NÃO vai parar aqui.
        // Ele vai direto para o banco SQLite via ClienteRepository.
        public static List<Cliente> Clientes = new List<Cliente>();
        public static List<Servico> Servicos = new List<Servico>();
        public static List<Agendamento> Agendamentos = new List<Agendamento>();
    }
}
