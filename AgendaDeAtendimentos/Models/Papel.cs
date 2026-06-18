namespace AgendaDeAtendimentos.Models
{
    // Classe responsável por representar os papéis (perfis)
    // que um usuário pode possuir dentro do sistema.
    public class Papel
    {
        // Identificador único do papel no banco de dados
        public int Id { get; set; }

        // Nome do papel.
        // Exemplo: Admin, Operador ou Visualizador.
        public string? Nome { get; set; }

        // Constante que representa o papel de Administrador.
        // Administradores normalmente possuem acesso total ao sistema.
        public const string ADMIN = "Admin";

        // Constante que representa o papel de Operador.
        // Operadores podem executar as operações do dia a dia.
        public const string OPERADOR = "Operador";

        // Constante que representa o papel de Visualizador.
        // Visualizadores possuem apenas permissão de consulta.
        public const string VISUALIZADOR = "Visualizador";

        // Construtor vazio.
        // Permite criar um objeto Papel sem informar dados inicialmente.
        public Papel() { }

        // Construtor com parâmetros.
        // Facilita a criação de um papel já preenchendo seus dados.
        public Papel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}