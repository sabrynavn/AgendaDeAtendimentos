using System;
using Microsoft.Data.Sqlite; // Provedor de dados oficial da Microsoft para o SQLite
using AgendaDeAtendimentos.Models; // Namespace das classes Usuario e Papel
using AgendaDeAtendimentos.Data; // Namespace da configuração de banco de dados

namespace AgendaDeAtendimentos.Repositories
{
    public class UsuarioRepository
    {
        // Este método recebe o login digitado na tela, faz uma busca segura no banco SQLite,
        // monta os objetos 'Usuario' e 'Papel' com os dados reais e devolve tudo pronto.
        public Usuario? BuscarPorLogin(string login)
        {
            // Criamos o comando SQL usando INNER JOIN para buscar o usuário e o seu papel de uma só vez
            // O LOWER() garante que o sistema aceite 'ADMIN' ou 'admin' da mesma forma
            string sql = @"
                SELECT u.id, u.nome, u.login, u.senha_hash, u.ativo, u.papel_id, p.nome AS nome_papel
                FROM usuarios u
                INNER JOIN papeis p ON u.papel_id = p.id
                WHERE LOWER(u.login) = LOWER(@login) AND u.ativo = 1 LIMIT 1;";

            // O bloco 'using' abre a conexão e garante que ela seja FECHADA automaticamente no final
            using (SqliteConnection conexao = DatabaseConfig.ObterConexao())
            {
                conexao.Open(); // Abre os canais com o arquivo .db

                using (var comando = new SqliteCommand(sql, conexao))
                {
                    // PROTEÇÃO ANTI-HACKER: Passamos o parâmetro de forma segura para evitar SQL Injection
                    comando.Parameters.AddWithValue("@login", login);

                    // Executa o comando e começa a ler as colunas retornadas do SQLite
                    using (var leitor = comando.ExecuteReader())
                    {
                        // Se o banco de dados encontrou uma linha correspondente...
                        if (leitor.Read())
                        {
                            // 1. Instanciamos o objeto Papel usando o construtor parametrizado criado por ela!
                            var papel = new Papel(
                                id: Convert.ToInt32(leitor["papel_id"]),
                                nome: leitor["nome_papel"].ToString() ?? string.Empty
                            );

                            // 2. Instanciamos o objeto Usuario usando o construtor parametrizado criado por ela!
                            var usuario = new Usuario(
                                nome: leitor["nome"].ToString(),
                                login: leitor["login"].ToString(),
                                senhaHash: leitor["senha_hash"].ToString(),
                                papelId: papel.Id
                            );

                            // 3. Preenchemos as propriedades restantes do modelo
                            usuario.Id = Convert.ToInt32(leitor["id"]);
                            usuario.Ativo = Convert.ToInt32(leitor["ativo"]) == 1; // Transforma o número 1 do banco em true no C#

                            // 4. VÍNCULO DO OBJETO: Aqui associamos o papel completo ao usuário de forma limpa.
                            // CORREÇÃO: Removido o 'Paper' com R que estava gerando o erro de compilação!
                            usuario.Papel = papel;

                            return usuario; // Retorna o usuário perfeitamente montado
                        }
                    }
                }
            }

            return null; // Retorna nulo se o login digitado não existir no banco
        }
    }
}