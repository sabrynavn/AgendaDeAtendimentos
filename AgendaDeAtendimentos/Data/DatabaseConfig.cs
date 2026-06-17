using Microsoft.Data.Sqlite;


namespace AgendaDeAtendimentos.Data
{
    // Classe responsável por configurar e inicializar o banco de dados.
    // Como ela é static, seus métodos podem ser chamados sem criar um objeto.
    public static class DatabaseConfig
    {
        // String de conexão utilizada para acessar o banco SQLite.
        // O arquivo do banco será criado com o nome sistema.db.
        private const string ConnectionString = "Data Source=sistema.db";

        // Método responsável por iniciar o banco de dados.
        // Ele abre a conexão, cria as tabelas e insere os dados iniciais.
        public static void InicializarBanco()
        {
            using var conexao = new SqliteConnection(ConnectionString);

            // Abre a conexão com o banco.
            conexao.Open();

            // Cria as tabelas caso ainda não existam.
            CriarTabelas(conexao);

            // Insere os dados padrões do sistema.
            InserirDadosIniciais(conexao);
        }

        // Método responsável por criar todas as tabelas do sistema.
        private static void CriarTabelas(SqliteConnection conexao)
        {
            // Tabela que armazena os papéis/perfis dos usuários.
            Executar(conexao, @"CREATE TABLE IF NOT EXISTS papeis (
                id   INTEGER PRIMARY KEY AUTOINCREMENT,
                nome TEXT NOT NULL UNIQUE);");

            // Tabela de usuários do sistema.
            Executar(conexao, @"CREATE TABLE IF NOT EXISTS usuarios (
                id         INTEGER PRIMARY KEY AUTOINCREMENT,
                nome       TEXT    NOT NULL,
                login      TEXT    NOT NULL UNIQUE,
                senha_hash TEXT    NOT NULL,
                papel_id   INTEGER NOT NULL,
                ativo      INTEGER NOT NULL DEFAULT 1,
                FOREIGN KEY (papel_id) REFERENCES papeis(id));");

            // Tabela que armazena os clientes cadastrados.
            Executar(conexao, @"CREATE TABLE IF NOT EXISTS clientes (
                id       INTEGER PRIMARY KEY AUTOINCREMENT,
                nome     TEXT NOT NULL,
                telefone TEXT,
                email    TEXT);");

            // Tabela responsável pelos serviços oferecidos.
            Executar(conexao, @"CREATE TABLE IF NOT EXISTS servicos (
                id          INTEGER PRIMARY KEY AUTOINCREMENT,
                nome        TEXT NOT NULL,
                descricao   TEXT,
                duracao_min INTEGER NOT NULL DEFAULT 30,
                valor       REAL    NOT NULL DEFAULT 0);");

            // Tabela que registra os agendamentos realizados.
            // Cada agendamento está ligado a um cliente e um serviço.
            Executar(conexao, @"CREATE TABLE IF NOT EXISTS agendamentos (
                id         INTEGER PRIMARY KEY AUTOINCREMENT,
                cliente_id INTEGER NOT NULL,
                servico_id INTEGER NOT NULL,
                data_hora  TEXT    NOT NULL,
                status     TEXT    NOT NULL DEFAULT 'Agendado',
                observacao TEXT,
                FOREIGN KEY (cliente_id) REFERENCES clientes(id),
                FOREIGN KEY (servico_id) REFERENCES servicos(id));");
        }

        // Método responsável por inserir dados iniciais necessários
        // para o funcionamento do sistema.
        private static void InserirDadosIniciais(SqliteConnection conexao)
        {
            // Verifica quantos papéis existem cadastrados.
            long qtdPapeis = (long)new SqliteCommand(
                "SELECT COUNT(*) FROM papeis;", conexao).ExecuteScalar()!;

            // Se não existir nenhum papel, cadastra os padrões.
            if (qtdPapeis == 0)
            {
                Executar(conexao, @"
                    INSERT INTO papeis (id, nome) VALUES (1, 'Admin');
                    INSERT INTO papeis (id, nome) VALUES (2, 'Operador');
                    INSERT INTO papeis (id, nome) VALUES (3, 'Visualizador');");
            }

            // Verifica se já existe algum usuário cadastrado.
            long qtdUsuarios = (long)new SqliteCommand(
                "SELECT COUNT(*) FROM usuarios;", conexao).ExecuteScalar()!;

            // Caso não exista, cria um usuário administrador padrão.
            if (qtdUsuarios == 0)
            {
                // Gera o hash da senha para aumentar a segurança.
                string hash = BCrypt.Net.BCrypt.HashPassword("admin123");

                var cmd = new SqliteCommand(@"
                    INSERT INTO usuarios (nome, login, senha_hash, papel_id, ativo)
                    VALUES ('Administrador', 'admin', @hash, 1, 1);", conexao);

                // Adiciona o hash como parâmetro da consulta.
                cmd.Parameters.AddWithValue("@hash", hash);

                // Executa o comando de inserção.
                cmd.ExecuteNonQuery();
            }
        }

        // Método auxiliar utilizado para executar comandos SQL
        // que não retornam dados, como CREATE, INSERT ou UPDATE.
        private static void Executar(SqliteConnection conexao, string sql)
        {
            using var cmd = new SqliteCommand(sql, conexao);
            cmd.ExecuteNonQuery();
        }

        // Retorna uma nova conexão com o banco de dados.
        // Pode ser utilizada pelos repositórios do sistema.
        public static SqliteConnection ObterConexao()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}