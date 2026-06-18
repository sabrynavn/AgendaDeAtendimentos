using System.Collections.Generic;
using AgendaDeAtendimentos.Models;

namespace AgendaDeAtendimentos.Repositories
{
    // Interface = contrato que define o que o repositório deve fazer
    public interface IUsuarioRepository
    {
        Usuario? BuscarPorLogin(string login);
        Usuario? BuscarPorId(int id);
        List<Usuario> ListarTodos();
        List<Papel> ListarPapeis();
        void Inserir(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Desativar(int id);
    }
}