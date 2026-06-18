using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using AgendaDeAtendimentos.Models;
using AgendaDeAtendimentos.Data;

namespace AgendaDeAtendimentos.Repositories
{
    // Repositório responsável por todas as operações relacionadas aos usuários
    // Aqui ficam as consultas ao banco (buscar, listar, inserir, atualizar, etc.)
    public class UsuarioRepository : IUsuarioRepository
    {
        // Busca um usuário pelo login
        // Utilizado principalmente durante o processo de autenticação
        public Usuario? BuscarPorLogin(string login)
        {
            string sql = @"
SELECT u.id, u.nome, u.login, u.senha_hash, u.ativo, u.papel_id, p.nome AS nome_papel
FROM usuarios u
INNER JOIN papeis p ON u.papel_id = p.id
WHERE LOWER(u.login) = LOWER(@login) AND u.ativo = 1 LIMIT 1;";


        // Obtém uma conexão com o banco de dados
        using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            // Cria o comando SQL
            using var comando = new SqliteCommand(sql, conexao);

            // Substitui o parâmetro @login pelo valor recebido no método
            // Isso ajuda a evitar SQL Injection
            comando.Parameters.AddWithValue("@login", login);

            // Executa a consulta
            using var leitor = comando.ExecuteReader();

            // Se encontrar um registro, converte para objeto Usuario
            if (leitor.Read())
                return Mapear(leitor);

            // Caso não encontre nenhum usuário
            return null;
        }

        // Busca um usuário pelo ID
        // Pode ser utilizado em telas de edição ou visualização
        public Usuario? BuscarPorId(int id)
        {
            string sql = @"
            SELECT u.id, u.nome, u.login, u.senha_hash, u.ativo, u.papel_id, p.nome AS nome_papel
            FROM usuarios u
            INNER JOIN papeis p ON u.papel_id = p.id
            WHERE u.id = @id LIMIT 1;";

            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(sql, conexao);

            // Substitui o parâmetro @id pelo valor informado
            comando.Parameters.AddWithValue("@id", id);

            using var leitor = comando.ExecuteReader();

            if (leitor.Read())
                return Mapear(leitor);

            return null;
        }

        // Retorna todos os usuários cadastrados no banco
        // Utilizado em telas administrativas
        public List<Usuario> ListarTodos()
        {
            var lista = new List<Usuario>();

            string sql = @"
            SELECT u.id, u.nome, u.login, u.senha_hash, u.ativo, u.papel_id, p.nome AS nome_papel
            FROM usuarios u
            INNER JOIN papeis p ON u.papel_id = p.id
            ORDER BY u.nome;";

            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(sql, conexao);
            using var leitor = comando.ExecuteReader();

            // Percorre todas as linhas retornadas pela consulta
            while (leitor.Read())
            {
                lista.Add(Mapear(leitor));
            }

            return lista;
        }

        // Lista todos os papéis cadastrados
        // Exemplo: Admin, Operador e Visualizador
        // Utilizado para preencher ComboBox de cadastro de usuário
        public List<Papel> ListarPapeis()
        {
            var lista = new List<Papel>();

            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "SELECT id, nome FROM papeis ORDER BY id;",
                conexao);

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new Papel(
                    id: Convert.ToInt32(leitor["id"]),
                    nome: leitor["nome"].ToString() ?? ""
                ));
            }

            return lista;
        }

        // Insere um novo usuário no banco de dados
        public void Inserir(Usuario usuario)
        {
            string sql = @"
            INSERT INTO usuarios (nome, login, senha_hash, papel_id, ativo)
            VALUES (@nome, @login, @hash, @papelId, 1);";

            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(sql, conexao);

            // Preenche os parâmetros da consulta com os dados do objeto
            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@login", usuario.Login);
            comando.Parameters.AddWithValue("@hash", usuario.SenhaHash);
            comando.Parameters.AddWithValue("@papelId", usuario.PapelId);

            // Executa o INSERT
            comando.ExecuteNonQuery();
        }

        // Atualiza os dados de um usuário já existente
        public void Atualizar(Usuario usuario)
        {
            string sql = @"
            UPDATE usuarios
            SET nome = @nome,
                login = @login,
                senha_hash = @hash,
                papel_id = @papelId
            WHERE id = @id;";

            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(sql, conexao);

            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@login", usuario.Login);
            comando.Parameters.AddWithValue("@hash", usuario.SenhaHash);
            comando.Parameters.AddWithValue("@papelId", usuario.PapelId);
            comando.Parameters.AddWithValue("@id", usuario.Id);

            // Executa o UPDATE no banco
            comando.ExecuteNonQuery();
        }

        // Em vez de excluir o usuário do banco,
        // apenas altera o campo ativo para 0
        // Isso é chamado de exclusão lógica
        public void Desativar(int id)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            using var comando = new SqliteCommand(
                "UPDATE usuarios SET ativo = 0 WHERE id = @id;",
                conexao);

            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
        }

        // Converte uma linha retornada pelo banco em um objeto Usuario
        // Esse processo é conhecido como mapeamento
        private Usuario Mapear(SqliteDataReader leitor)
        {
            // Cria o objeto Papel utilizando os dados retornados da consulta
            var papel = new Papel(
                id: Convert.ToInt32(leitor["papel_id"]),
                nome: leitor["nome_papel"].ToString() ?? ""
            );

            // Cria o objeto Usuario
            var usuario = new Usuario(
                nome: leitor["nome"].ToString(),
                login: leitor["login"].ToString(),
                senhaHash: leitor["senha_hash"].ToString(),
                papelId: papel.Id
            );

            // Preenche as demais propriedades
            usuario.Id = Convert.ToInt32(leitor["id"]);

            // Se ativo for 1 retorna true, se for 0 retorna false
            usuario.Ativo = Convert.ToInt32(leitor["ativo"]) == 1;

            // Associa o objeto Papel ao usuário
            usuario.Papel = papel;

            return usuario;
        }
    }

}
