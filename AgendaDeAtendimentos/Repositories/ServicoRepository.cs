// ============================================================
// Repositories/ServicoRepository.cs
// ============================================================
//
// O QUE É UM REPOSITORY?
// Mesma ideia do ClienteRepository, mas agora para serviços.
//
// REPARE NO PADRÃO:
// - ListarTodos() → SELECT sem WHERE
// - BuscarPorId() → SELECT com WHERE
// - Inserir() → INSERT
// - Atualizar() → UPDATE
// - Excluir() → DELETE
// Esse é o padrão CRUD (Create, Read, Update, Delete) e se repete
// em TODOS os repositórios. Depois que você aprende um, aprendeu todos.

using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using AgendaDeAtendimentos.Models;
using AgendaDeAtendimentos.Data;

namespace AgendaDeAtendimentos.Repositories
{
    public class ServicoRepository
    {
        // ------------------------------------------
        // LISTAR TODOS OS SERVIÇOS
        // ------------------------------------------
        // Busca todos os serviços cadastrados no banco, ordenados por nome.
        public List<Servico> ListarTodos()
        {
            var lista = new List<Servico>();

            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            // A tabela "servicos" tem as colunas: id, nome, descricao, duracao_min, valor.
            // Mas a gente só usa id, nome e valor no sistema (descricao e duracao_min
            // estão no banco mas não no model atual).
            using var comando = new SqliteCommand(
                "SELECT id, nome, descricao, duracao_min, valor FROM servicos ORDER BY nome;",
                conexao);

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
                lista.Add(Mapear(leitor));

            return lista;
        }


        // ------------------------------------------
        // BUSCAR SERVIÇO POR ID
        // ------------------------------------------
        public Servico? BuscarPorId(int id)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "SELECT id, nome, descricao, duracao_min, valor FROM servicos WHERE id = @id LIMIT 1;",
                conexao);

            comando.Parameters.AddWithValue("@id", id);

            using var leitor = comando.ExecuteReader();

            if (leitor.Read())
                return Mapear(leitor);

            return null;
        }


        // ------------------------------------------
        // INSERIR SERVIÇO
        // ------------------------------------------
        // Adiciona um novo serviço no banco.
        // ATENÇÃO: descricao e duracao_min estão sendo preenchidos com valores
        // fixos porque o formulário atual não tem campos para eles.
        // Num sistema completo, viriam da tela.
        public void Inserir(Servico servico)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "INSERT INTO servicos (nome, descricao, duracao_min, valor) VALUES (@nome, @descricao, @duracao, @valor);",
                conexao);

            comando.Parameters.AddWithValue("@nome", servico.Nome);
            comando.Parameters.AddWithValue("@descricao", ""); // Sem descrição por enquanto
            comando.Parameters.AddWithValue("@duracao", 30); // Duração padrão de 30 min
            comando.Parameters.AddWithValue("@valor", servico.Valor);

            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // ATUALIZAR SERVIÇO
        // ------------------------------------------
        public void Atualizar(Servico servico)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "UPDATE servicos SET nome = @nome, valor = @valor WHERE id = @id;",
                conexao);

            comando.Parameters.AddWithValue("@nome", servico.Nome);
            comando.Parameters.AddWithValue("@valor", servico.Valor);
            comando.Parameters.AddWithValue("@id", servico.Id);

            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // EXCLUIR SERVIÇO
        // ------------------------------------------
        public void Excluir(int id)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "DELETE FROM servicos WHERE id = @id;",
                conexao);

            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // MAPEAR
        // ------------------------------------------
        // Converte a linha do banco em um objeto Servico do C#.
        private static Servico Mapear(SqliteDataReader leitor)
        {
            return new Servico
            {
                Id = Convert.ToInt32(leitor["id"]),
                Nome = leitor["nome"].ToString() ?? "",
                // Convert.ToDecimal porque o banco devolve o valor como número decimal,
                // e o C# precisa de "decimal" para guardar dinheiro corretamente.
                Valor = Convert.ToDecimal(leitor["valor"])
            };
        }
    }
}
