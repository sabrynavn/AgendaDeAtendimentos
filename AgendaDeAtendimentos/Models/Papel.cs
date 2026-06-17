namespace AgendaDeAtendimentos.Models
{
    // Esta classe representa um papel (Role) dentro do sistema.
    // O papel define quais permissões um usuário possui.
    public class Papel
    {
        // Identificador único do papel no banco de dados. 1 = Admin , 2 = Operador ouu 3 = Visualizador
        public int Id { get; set; }

        // Nome do papel.
        // Armazena o nome que será exibido para o usuário.
        public string? Nome { get; set; }

        // Constantes para evitar escrever strings manualmente.
        // Isso ajuda a evitar erros de digitação e facilita manutenção.

        // Papel com acesso total ao sistema.
        public const string ADMIN = "Admin";

        // Papel que pode realizar operações do sistema,
        // mas não pode gerenciar usuários.
        public const string OPERADOR = "Operador";

        // Papel que possui apenas permissões de consulta.
        // Não pode cadastrar, editar ou excluir dados.
        public const string VISUALIZADOR = "Visualizador";

        // Construtor vazio.
        // Para criar um objeto Papel sem informar dados inicialmente.
        public Papel() { }

        // Construtor com parâmetros.
        // Permite criar um papel já preenchendo seus atributos. Exemplo: Papel admin = new Papel(1, "Admin");
        public Papel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        // Sobrescreve o método ToString().
        // Quando um objeto Papel for exibido em um componente visual, será mostrado
        // apenas o nome do papel Admin ao invés d eum AgendaDeAtendimentos.Models.Papel
        public override string ToString() => Nome;
    }
}