// ============================================================
// Repositories/ClienteRepository.cs
// ============================================================
//
// O QUE É UM REPOSITORY?
// Repository é a camada que FALA COM O BANCO DE DADOS.
// Toda vez que o programa precisa SALVAR, BUSCAR, ATUALIZAR ou EXCLUIR
// um cliente no banco SQLite, ele chama os métodos daqui.
//
// PENSE ASSIM:
// A tela (FormPrincipal) não pode falar direto com o banco - isso é bagunça.
// Ela pede para o Service, que pede para o Repository, que fala com o banco.
// Cada um tem sua responsabilidade.
//
// REGRA DE OURO:
// Aqui só tem SQL. A gente escreve o comando, manda executar e devolve o resultado.
// NÃO tem lógica de negócio aqui (tipo "só pode excluir se não tiver agendamento").
// Lógica de negócio vai no Service.

// "using System.Collections.Generic" = para usar List<T> (listas)
using System.Collections.Generic;
// "Microsoft.Data.Sqlite" = biblioteca que permite o C# conversar com SQLite
using Microsoft.Data.Sqlite;
// Nossos models (Cliente, etc.)
using AgendaDeAtendimentos.Models;
// DatabaseConfig - a classe que cria a conexão com o banco
using AgendaDeAtendimentos.Data;

// Tudo isso vive na pasta "Repositories" (organização em camadas)
namespace AgendaDeAtendimentos.Repositories
{
    // ClienteRepository: a classe que faz o CRUD de clientes no banco.
    // "public" = qualquer um pode usar.
    // "class" = é uma classe.
    public class ClienteRepository
    {
        // ------------------------------------------
        // LISTAR TODOS OS CLIENTES
        // ------------------------------------------
        // Busca todos os clientes do banco e devolve uma lista.
        // "List<Cliente>" = o método devolve uma lista de objetos Cliente.
        // "ListarTodos()" = nome do método (padrão: verbo + o que faz).
        public List<Cliente> ListarTodos()
        {
            // Cria uma lista vazia para ir adicionando os clientes encontrados.
            var lista = new List<Cliente>();

            // DatabaseConfig.ObterConexao() pega uma conexão nova com o banco.
            // "using" = quando terminar, fecha a conexão automaticamente (evita vazamento).
            // "var" = o C# descobre o tipo sozinho (SqliteConnection).
            using var conexao = DatabaseConfig.ObterConexao();

            // Abre a conexão - agora podemos falar com o banco.
            conexao.Open();

            // Cria um comando SQL para buscar todos os clientes em ordem alfabética.
            // "ORDER BY nome" = ordena por nome (A a Z).
            // "SqliteCommand" é um objeto que representa um comando SQL.
            using var comando = new SqliteCommand(
                "SELECT id, nome, telefone, email FROM clientes ORDER BY nome;",
                conexao);

            // Executa o comando e devolve um "leitor" (reader).
            // O leitor vai linha por linha no resultado.
            // É como se fosse um cursor que anda para baixo na tabela.
            using var leitor = comando.ExecuteReader();

            // Enquanto tiver uma linha para ler (leitor.Read() volta true),
            // converte a linha em um objeto Cliente e adiciona na lista.
            while (leitor.Read())
                lista.Add(Mapear(leitor));

            // Devolve a lista cheia de clientes.
            return lista;
        }


        // ------------------------------------------
        // BUSCAR CLIENTE POR ID
        // ------------------------------------------
        // Procura UM cliente pelo número de ID.
        // "Cliente?" (com ?) = pode devolver null se não achar.
        // "int id" = recebe o ID para procurar.
        public Cliente? BuscarPorId(int id)
        {
            // Abre conexão.
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            // Comando SQL com WHERE para filtrar por id.
            // "@id" é um parâmetro (proteção contra SQL Injection).
            // "LIMIT 1" = para de procurar depois de achar o primeiro (só pode ter um).
            using var comando = new SqliteCommand(
                "SELECT id, nome, telefone, email FROM clientes WHERE id = @id LIMIT 1;",
                conexao);

            // Substitui @id pelo valor do parâmetro.
            // Isso é seguro - se alguém tentar colocar "1; DROP TABLE" não funciona.
            comando.Parameters.AddWithValue("@id", id);

            // Executa a leitura.
            using var leitor = comando.ExecuteReader();

            // Se encontrou alguma linha (Read() = true), converte e devolve.
            if (leitor.Read())
                return Mapear(leitor);

            // Se não encontrou nada, devolve null (nada).
            return null;
        }


        // ------------------------------------------
        // INSERIR CLIENTE
        // ------------------------------------------
        // Adiciona um novo cliente no banco.
        // "void" = não devolve nada (só executa).
        // "Cliente cliente" = recebe o objeto Cliente com os dados.
        public void Inserir(Cliente cliente)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            // INSERT INTO - comando SQL para adicionar uma nova linha.
            // "VALUES (@nome, @telefone, @email)" = valores que vamos substituir.
            // Repare que NÃO passamos o id - o SQLite gera automaticamente (AUTOINCREMENT).
            using var comando = new SqliteCommand(
                "INSERT INTO clientes (nome, telefone, email) VALUES (@nome, @telefone, @email);",
                conexao);

            // Substitui cada @param pelo valor correspondente do objeto.
            comando.Parameters.AddWithValue("@nome", cliente.Nome);
            comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
            comando.Parameters.AddWithValue("@email", cliente.Email);

            // ExecuteNonQuery() = executa o comando e não espera resultado de volta.
            // Usado para INSERT, UPDATE, DELETE (comandos que não DEVOLVEM dados).
            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // ATUALIZAR CLIENTE
        // ------------------------------------------
        // Altera os dados de um cliente que já existe no banco.
        // Usa o Id para encontrar qual cliente alterar.
        public void Atualizar(Cliente cliente)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            // UPDATE - comando SQL para alterar dados existentes.
            // "SET nome = @nome, ..." = quais colunas vão mudar.
            // "WHERE id = @id" = em QUAL linha (pelo ID).
            using var comando = new SqliteCommand(
                "UPDATE clientes SET nome = @nome, telefone = @telefone, email = @email WHERE id = @id;",
                conexao);

            comando.Parameters.AddWithValue("@nome", cliente.Nome);
            comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
            comando.Parameters.AddWithValue("@email", cliente.Email);
            comando.Parameters.AddWithValue("@id", cliente.Id);

            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // EXCLUIR CLIENTE
        // ------------------------------------------
        // Remove um cliente do banco pelo ID.
        // CUIDADO: exclusão permanente! Não tem como desfazer.
        public void Excluir(int id)
        {
            using var conexao = DatabaseConfig.ObterConexao();
            conexao.Open();

            // DELETE - comando SQL para remover uma linha.
            using var comando = new SqliteCommand(
                "DELETE FROM clientes WHERE id = @id;",
                conexao);

            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
        }


        // ------------------------------------------
        // MAPEAR (CONVERTER LINHA DO BANCO EM OBJETO)
        // ------------------------------------------
        // Esse método é "private" = SÓ essa classe pode usar.
        // "static" = não precisa criar um objeto ClienteRepository para usar.
        //
        // Ele recebe um SqliteDataReader (uma linha do banco) e converte
        // para um objeto Cliente (que o C# entende).
        //
        // PENSE: O banco devolve TEXTO (colunas), mas o C# trabalha com OBJETOS.
        // Esse método faz a tradução entre os dois mundos.
        private static Cliente Mapear(SqliteDataReader leitor)
        {
            // Cria um novo Cliente e preenche as propriedades com os valores do banco.
            // leitor["id"] = pega o valor da coluna "id" da linha atual.
            // Convert.ToInt32 = converte o valor para inteiro (já que no banco pode vir como long).
            // leitor["nome"].ToString() ?? "" = pega o nome, se for nulo usa "" vazio.
            return new Cliente
            {
                Id = Convert.ToInt32(leitor["id"]),
                Nome = leitor["nome"].ToString() ?? "",
                Telefone = leitor["telefone"].ToString() ?? "",
                Email = leitor["email"].ToString() ?? ""
            };
        }
    }
}
