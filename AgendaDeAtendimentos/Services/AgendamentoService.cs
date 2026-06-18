// ============================================================
// Services/AgendamentoService.cs
// ============================================================
//
// SERVICE DE AGENDAMENTO
// Mesma ideia dos outros, mas com uma diferença:
// O método Salvar() trata o "atualizar" de forma diferente.
//
// No Cliente e Serviço, "atualizar" significa mudar TUDO.
// No Agendamento, a gente só pode mudar o STATUS (não pode mudar
// o cliente, serviço ou data depois de criado - por enquanto).
//
// Por isso o Salvar de Agendamento chama Inserir para novo
// e AtualizarStatus para existente.

using System.Collections.Generic;
using AgendaDeAtendimentos.Models;
using AgendaDeAtendimentos.Repositories;

namespace AgendaDeAtendimentos.Services
{
    public class AgendamentoService
    {
        private readonly AgendamentoRepository _repository;

        public AgendamentoService()
        {
            _repository = new AgendamentoRepository();
        }

        // Lista todos os agendamentos (já vem com cliente e serviço dentro).
        public List<Agendamento> ListarTodos() => _repository.ListarTodos();

        // Salvar: insere novo ou, se já existe, só atualiza o status.
        public void Salvar(Agendamento agendamento)
        {
            if (agendamento.Id == 0)
                _repository.Inserir(agendamento);
            else
                // Se já existe, só pode atualizar o status.
                // Não permitimos mudar cliente, serviço ou data depois de criado.
                _repository.AtualizarStatus(agendamento.Id, agendamento.Status);
        }

        // AtualizarStatus: método separado para quando a tela clica em "Atualizar Status".
        // Recebe o id e o novo status e manda atualizar no banco.
        public void AtualizarStatus(int id, string status)
        {
            _repository.AtualizarStatus(id, status);
        }

        // Excluir um agendamento pelo ID.
        public void Excluir(int id) => _repository.Excluir(id);
    }
}
