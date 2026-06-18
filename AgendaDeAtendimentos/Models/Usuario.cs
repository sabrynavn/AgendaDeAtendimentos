namespace AgendaDeAtendimentos.Models
{
    // Classe que representa um usuário do sistema.
    // Cada usuário possui um login, uma senha protegida por hash
    // e um papel que define suas permissões.
    public class Usuario
    {
        // Identificador único do usuário no banco de dados.
        public int Id { get; set; }

        // O símbolo ? indica que esta propriedade pode ser nula.
        public string? Nome { get; set; }

        // Login utilizado para acessar o sistema.
        // Exemplo: "admin", "joao.silva", etc.
        public string? Login { get; set; }

        // Armazena o hash da senha.
        // NUNCA SALVE a senha original por questões de segurança.
        // O BCrypt será responsável por gerar esse hash. tipo um 1wwjdjoak#%$d
        public string? SenhaHash { get; set; }

        // Chave estrangeira que relaciona o usuário a um papel. 1 = Admin , 2 = Operador ou 3 = Visualizador
        public int PapelId { get; set; }

        // Objeto completo do papel do usuário.
        // O símbolo ? indica que inicialmente ele pode ser nulo.
        // Normalmente será preenchido pelo Repository após buscar os dados do banco.
        public Papel? Papel { get; set; }

        // Indica se o usuário está ativo no sistema.
        public bool Ativo { get; set; } = true;

        // Construtor vazio.
        // Permite criar um objeto Usuario sem passar informações inicialmente.
        public Usuario() { }

        // Construtor com parâmetros.
        // Facilita a criação de um usuário já preenchendo os principais dados.
        public Usuario(string? nome, string? login, string? senhaHash, int papelId)
        {
            Nome = nome;
            Login = login;
            SenhaHash = senhaHash;
            PapelId = papelId;

            // Todo usuário criado através deste construtor inicia ativo.
            Ativo = true;
        }

        // Verifica se o usuário possui o papel de algum cargo
        // O operador ?. evita erro caso Papel seja nulo.
        // true  -> se for x cargo
        // false -> caso contrário
        public bool IsAdmin()
            => Papel?.Nome == Papel.ADMIN;
        public bool IsOperador()
            => Papel?.Nome == Papel.OPERADOR;
        public bool IsVisualizador()
            => Papel?.Nome == Papel.VISUALIZADOR;

        // Sobrescreve o método ToString().
        // Define como o objeto será exibido quando convertido para texto
        // João Silva (Admin)
        public override string ToString()
            => $"{Nome} ({Papel?.Nome})";
    }
}