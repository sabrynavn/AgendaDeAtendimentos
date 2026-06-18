using AgendaDeAtendimentos.Models;
using AgendaDeAtendimentos.Repositories;

namespace AgendaDeAtendimentos.Services
{
    public class AuthService
    {
        // Usuário logado agora — null = ninguém logado
        public static Usuario? UsuarioAtual { get; private set; }

        private readonly UsuarioRepository _repositorio;

        public AuthService()
        {
            _repositorio = new UsuarioRepository();
        }

        // Retorna true se login e senha estiverem corretos
        public bool Login(string login, string senha)
        {
            // 1. Busca no banco pelo login
            var usuario = _repositorio.BuscarPorLogin(login);

            // 2. Se não encontrou, nega
            if (usuario == null) return false;

            // 3. BCrypt compara a senha digitada com o hash do banco
            bool senhaCorreta = BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash ?? "");

            // 4. Se senha errada, nega
            if (!senhaCorreta) return false;

            // 5. Salva o usuário na sessão
            UsuarioAtual = usuario;
            return true;
        }

        // Encerra a sessão
        public static void Logout()
        {
            UsuarioAtual = null;
        }

        public static bool EstaLogado => UsuarioAtual != null;
        public static bool IsAdmin() => UsuarioAtual?.Papel?.Nome == Papel.ADMIN;
        public static bool IsOperador() => UsuarioAtual?.Papel?.Nome == Papel.OPERADOR || IsAdmin();
        public static bool IsVisualizador() => EstaLogado;
    }
}