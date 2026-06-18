using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite; //Permite usar os comandos do sqlite


namespace AgendaDeAtendimentos.Data
{
    public static class DatabaseConfig
    {
        //Define o nome e o caminho do arquivo do banco de dados (.db)
        //O "Data Source=sistema.db" cria o arquivo diretamente na pasta onde o programa roda
        private const string ConnectionString = "Data Source =sistema.db";

        // Método principal que será chamado quando o programa iniciar
        public static void InicializarBanco()
        {
            // Abre uma conexão segura com o arquivo de banco de dados
            using (var conexao = new SqliteConnection(ConnectionString))
            {
                conexao.Open();

                //1. CRIAR A TABELA DE PAPEIS
                //Cria a tabela se ela ainda não existir no arquivo
                string sqlTabelaPapeis = @"
                    CREATE TABLE IF NOT EXISTS papeis (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        nome TEXT NOT NULL UNIQUE
                );";

                using (var comando = new SqliteCommand(sqlTabelaPapeis, conexao))
                {
                    comando.ExecuteNonQuery(); //Execulta o comando sql no banco
                }

                //2. CRIAR TABELA DE USUARIOS
                //O papel_id é uma Chave Estrangeira que aponta para a tabela de papeis
                string sqlTabelaUsuarios = @"
                CREATE TABLE IF NOT EXISTS usuarios(
                    id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    nome TEXT NOT NULL,
                    login TEXT NOT NULL UNIQUE,
                    senha_hash TEXT NOT NULL,
                    papel_id INTEGER NOT NULL,
                    ativo INTEGER NOT NULL DEFAULT 1,
                    FOREIGN KEY (papel_id) REFERENCES papeis(id)
                );";

                using (var comando = new SqliteCommand(sqlTabelaUsuarios, conexao))
                {
                    comando.ExecuteNonQuery();
                }

                //3. INSERIR DADOS INICIAIS SE ESTIVEREM VAZIOS
                //Garante que o banco nasça com os papeis e o admin padrão cadastrados
                InserirDadosIniciais(conexao);
            }
        }

        private static void InserirDadosIniciais(SqliteConnection conexao)
        {
            // Verifica se a tabela de papéis já tem registros
            string sqlContarPapeis = "SELECT COUNT(*) FROM papeis;";
            long quantidadePapeis = 0;

            using (var comando = new SqliteCommand(sqlContarPapeis, conexao))
            {
                quantidadePapeis = (long)comando.ExecuteScalar();
            }

            // Se a tabela estiver zerada, insere os 3 níveis obrigatórios
            if (quantidadePapeis == 0)
            {
                string sqlInserirPapeis = @"
                    INSERT INTO papeis (id, nome) VALUES (1, 'Admin');
                    INSERT INTO papeis (id, nome) VALUES (2, 'Operador');
                    INSERT INTO papeis (id, nome) VALUES (3, 'Visualizador');";

                using (var comando = new SqliteCommand(sqlInserirPapeis, conexao))
                {
                    comando.ExecuteNonQuery();
                }
            }
        }
        // Método público que permite a qualquer outra camada do sistema pegar a conexão com o banco
        public static SqliteConnection ObterConexao()       
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}
