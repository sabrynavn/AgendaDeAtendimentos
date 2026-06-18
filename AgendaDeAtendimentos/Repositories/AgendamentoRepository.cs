// ============================================================
// Repositories/AgendamentoRepository.cs
// ============================================================
//
// O QUE TEM DE DIFERENTE AQUI?
// Agendamento é mais complexo porque ele se CONECTA com outras tabelas.
// Um agendamento TEM um cliente e TEM um serviço.
// Para trazer os dados do cliente e do serviço JUNTO, usamos JOIN no SQL.
//
// JOIN (junção) é quando a gente pega dados de duas ou mais tabelas
// e junta num único resultado. É como se fosse um "cola" entre tabelas.
//
// EXEMPLO:
// Tabela agendamentos: [id, cliente_id, servico_id, data_hora]
// Tabela clientes: [id, nome, telefone]
// Tabela servicos: [id, nome, valor]
//
// Com JOIN, a gente faz uma consulta que devolve:
// [agendamento_id, data_hora, cliente_nome, servico_nome, servico_valor]

using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using AgendaDeAtendimentos.Models;
using AgendaDeAtendimentos.Data;

namespace AgendaDeAtendimentos.Repositories
{
    public class AgendamentoRepository
    {
        // ------------------------------------------
        // LISTAR TODOS OS AGENDAMENTOS
        // ------------------------------------------
        // Busca todos os agendamentos, incluindo o nome do cliente e do serviço.
        // Usa INNER JOIN para trazer os dados das outras tabelas.
        public List<Agendamento> ListarTodos()
        {
            var lista = new List<Agendamento>();

            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            // SQL com JOIN:
            // "a." = apelido para agendamentos
            // "c." = apelido para clientes
            // "s." = apelido para servicos
            //
            // INNER JOIN clientes c ON a.cliente_id = c.id
            // Só traz agendamentos que TENHAM um cliente válido.
            // Se o cliente foi excluído, o agendamento não aparece.
            //
            // "AS cliente_nome" = o resultado da coluna c.nome vai aparecer
            // com o nome "cliente_nome" para não confundir com outros "nome".
            string sql = @"
                SELECT a.id, a.cliente_id, a.servico_id, a.data_hora, a.status, a.observacao,
                       c.nome AS cliente_nome, c.telefone AS cliente_telefone,
                       s.nome AS servico_nome, s.valor AS servico_valor
                FROM agendamentos a
                INNER JOIN clientes c ON a.cliente_id = c.id
                INNER JOIN servicos s ON a.servico_id = s.id
                ORDER BY a.data_hora;";

            using var comando = new SqliteCommand(sql, conexao);
            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
                lista.Add(Mapear(leitor));

            return lista;
        }


        // ------------------------------------------
        // BUSCAR AGENDAMENTO POR ID
        // ------------------------------------------
        public Agendamento? BuscarPorId(int id)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            // Mesmo SQL do ListarTodos, mas com filtro WHERE a.id = @id
            string sql = @"
                SELECT a.id, a.cliente_id, a.servico_id, a.data_hora, a.status, a.observacao,
                       c.nome AS cliente_nome, c.telefone AS cliente_telefone,
                       s.nome AS servico_nome, s.valor AS servico_valor
                FROM agendamentos a
                INNER JOIN clientes c ON a.cliente_id = c.id
                INNER JOIN servicos s ON a.servico_id = s.id
                WHERE a.id = @id LIMIT 1;";

            using var comando = new SqliteCommand(sql, conexao);
            comando.Parameters.AddWithValue("@id", id);

            using var leitor = comando.ExecuteReader();

            if (leitor.Read())
                return Mapear(leitor);

            return null;
        }


        // ------------------------------------------
        // INSERIR AGENDAMENTO
        // ------------------------------------------
        public void Inserir(Agendamento agendamento)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "INSERT INTO agendamentos (cliente_id, servico_id, data_hora, status, observacao) VALUES (@clienteId, @servicoId, @dataHora, @status, @observacao);",
                conexao);

            comando.Parameters.AddWithValue("@clienteId", agendamento.ClienteId);
            comando.Parameters.AddWithValue("@servicoId", agendamento.ServicoId);

            // A data precisa ser formatada como texto no formato do SQLite.
            // "yyyy-MM-dd HH:mm:ss" = exemplo: "2025-12-25 14:30:00"
            // Se não formatar, o SQLite pode não entender a data.
            comando.Parameters.AddWithValue("@dataHora", agendamento.DataHora.ToString("yyyy-MM-dd HH:mm:ss"));

            comando.Parameters.AddWithValue("@status", agendamento.Status);
            comando.Parameters.AddWithValue("@observacao", agendamento.Observacao);

            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // ATUALIZAR STATUS
        // ------------------------------------------
        // Diferente dos outros repositórios, aqui NÃO temos um método
        // "Atualizar" genérico. Só atualizamos o status do agendamento.
        // Isso porque o sistema só permite mudar o status (por enquanto).
        public void AtualizarStatus(int id, string status)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "UPDATE agendamentos SET status = @status WHERE id = @id;",
                conexao);

            comando.Parameters.AddWithValue("@status", status);
            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // EXCLUIR AGENDAMENTO
        // ------------------------------------------
        public void Excluir(int id)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "DELETE FROM agendamentos WHERE id = @id;",
                conexao);

            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // MAPEAR
        // ------------------------------------------
        // Esse é o mapeamento mais complexo porque precisa construir
        // TRÊS objetos a partir de UMA linha do banco:
        // 1. Cliente (só com os dados básicos: id, nome, telefone)
        // 2. Servico (só com os dados básicos: id, nome, valor)
        // 3. Agendamento (com os dados do agendamento + os objetos Cliente e Servico dentro)
        private static Agendamento Mapear(SqliteDataReader leitor)
        {
            // Primeiro cria o objeto Cliente com os dados vindos do JOIN.
            // Esses dados vieram da tabela clientes, mas com apelidos (cliente_nome, etc.)
            var cliente = new Cliente
            {
                Id = Convert.ToInt32(leitor["cliente_id"]),
                Nome = leitor["cliente_nome"].ToString() ?? "",
                Telefone = leitor["cliente_telefone"].ToString() ?? ""
            };

            // Depois cria o objeto Servico com os dados da tabela servicos.
            var servico = new Servico
            {
                Id = Convert.ToInt32(leitor["servico_id"]),
                Nome = leitor["servico_nome"].ToString() ?? "",
                Valor = Convert.ToDecimal(leitor["servico_valor"])
            };

            // Por fim, cria o Agendamento com TUDO:
            // - Seus próprios dados (id, data, status, etc.)
            // - O objeto Cliente (para exibir o nome na tela)
            // - O objeto Servico (para exibir o nome e valor na tela)
            return new Agendamento
            {
                Id = Convert.ToInt32(leitor["id"]),
                ClienteId = cliente.Id,
                ServicoId = servico.Id,
                // DateTime.Parse converte o texto "2025-12-25 14:30:00" em um objeto DateTime.
                // O "!" no final (null-forgiving) diz ao C#: "confia, não vai ser nulo".
                DataHora = DateTime.Parse(leitor["data_hora"].ToString()!),
                Status = leitor["status"].ToString() ?? "Agendado",
                Observacao = leitor["observacao"].ToString() ?? "",
                Cliente = cliente,   // Guarda o objeto cliente completo
                Servico = servico    // Guarda o objeto servico completo
            };
        }
    }
}
