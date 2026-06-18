// ============================================================
// Services/ServicoService.cs
// ============================================================
//
// MESMA IDEIA DO ClienteService, mas para Serviço.
//
// REPAROU O PADRÃO?
// - Service tem um _repository do tipo correspondente
// - Service chama os métodos do repository
// - Service pode ter lógica extra (como o Salvar que decide entre inserir/atualizar)
//
// ISSO SE CHAMA "ARQUITETURA EM CAMADAS":
// Form → Service → Repository → Banco
// Cada um só fala com o vizinho direto. A tela NÃO pode falar direto com o banco.

using System.Collections.Generic;
using AgendaDeAtendimentos.Models;
using AgendaDeAtendimentos.Repositories;

namespace AgendaDeAtendimentos.Services
{
    public class ServicoService
    {
        private readonly ServicoRepository _repository;

        public ServicoService()
        {
            _repository = new ServicoRepository();
        }

        // Lista todos os serviços.
        public List<Servico> ListarTodos() => _repository.ListarTodos();

        // Salvar: se Id = 0, insere. Senão, atualiza.
        public void Salvar(Servico servico)
        {
            if (servico.Id == 0)
                _repository.Inserir(servico);
            else
                _repository.Atualizar(servico);
        }

        // Excluir um serviço pelo ID.
        public void Excluir(int id) => _repository.Excluir(id);
    }
}
