// ============================================================
// Services/ClienteService.cs
// ============================================================
//
// O QUE É UM SERVICE?
// Service é a camada do MEIO entre a tela (Form) e o banco (Repository).
//
// PENSE NELA COMO UM "GERENTE":
// - A tela (FormPrincipal) é o "atendente" - ela mostra os dados e recebe os cliques.
// - O Repository é o "arquivista" - ele guarda e busca os papéis no arquivo (banco).
// - O Service é o "gerente" - ele decide as regras: "pode salvar?", "precisa de algo antes?"
//
// VANTAGEM:
// Se amanhã precisar de uma regra tipo "só pode excluir cliente se não tiver agendamento",
// você coloca a lógica AQUI no Service, sem precisar mexer na tela nem no banco.

using System.Collections.Generic;
using AgendaDeAtendimentos.Models;
using AgendaDeAtendimentos.Repositories;

namespace AgendaDeAtendimentos.Services
{
    // ClienteService: o gerente dos clientes.
    public class ClienteService
    {
        // O Service GUARDA uma referência para o Repository.
        // "private" = só essa classe pode acessar.
        // "readonly" = depois que iniciar, não pode trocar (segurança).
        // "_repository" = convenção: underline na frente de variável privada.
        private readonly ClienteRepository _repository;

        // Construtor: quando criar um ClienteService, já cria o Repository junto.
        public ClienteService()
        {
            _repository = new ClienteRepository();
        }

        // ------------------------------------------
        // LISTAR TODOS
        // ------------------------------------------
        // Chama o repository e devolve a lista.
        // "=>" é syntax sugar para métodos de uma linha só.
        // É a mesma coisa que escrever:
        //   public List<Cliente> ListarTodos() { return _repository.ListarTodos(); }
        public List<Cliente> ListarTodos() => _repository.ListarTodos();

        // ------------------------------------------
        // SALVAR (INSERIR OU ATUALIZAR)
        // ------------------------------------------
        // LÓGICA DE NEGÓCIO:
        // Se o cliente NÃO tem ID (Id == 0), é um cliente NOVO → insere.
        // Se o cliente JÁ tem ID, é um cliente EXISTENTE → atualiza.
        //
        // Isso é útil porque QUEM CHAMA (a tela) não precisa se preocupar
        // em saber se é inserção ou atualização. É só chamar "Salvar" e pronto.
        public void Salvar(Cliente cliente)
        {
            // Id == 0 significa que o banco ainda não gerou um ID para ele.
            if (cliente.Id == 0)
                _repository.Inserir(cliente);   // Cliente novo → INSERT
            else
                _repository.Atualizar(cliente); // Cliente existente → UPDATE
        }

        // ------------------------------------------
        // EXCLUIR
        // ------------------------------------------
        // Exclui pelo ID (só repassa para o repository).
        // FUTURO: aqui poderia ter uma verificação tipo
        // "se cliente tem agendamento, não deixa excluir".
        public void Excluir(int id) => _repository.Excluir(id);
    }
}
