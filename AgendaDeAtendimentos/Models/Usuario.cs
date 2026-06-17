namespace AgendaDeAtendimentos.Models
{
    // Classe responsável por representar um usuário do sistema
    public class Usuario
    {
        // Identificador único do usuário no banco de dados
        public int Id { get; set; }

        // Nome completo do usuário
        public string? Nome { get; set; }

        // Login utilizado para acessar o sistema
        public string? Login { get; set; }

        // Senha armazenada em formato de hash para maior segurança
        public string? SenhaHash { get; set; }

        // Chave estrangeira que referencia o papel/permissão do usuário
        public int PapelId { get; set; }

        // Objeto que contém os dados do papel do usuário
        // Exemplo: Administrador, Operador ou Visualizador
        public Papel? Papel { get; set; }

        // Indica se o usuário está ativo no sistema
        // Por padrão todo usuário é criado como ativo
        public bool Ativo { get; set; } = true;

        // Propriedade auxiliar para facilitar o acesso ao nome do papel
        // sem precisar escrever Papel.Nome em outros locais do código
        public string PapelNome
        {
            // Retorna o nome do papel.
            // Se Papel for nulo, retorna uma string vazia.
            get => Papel?.Nome ?? "";

            set
            {
                // Se o objeto Papel ainda não existir,
                // cria uma nova instância para evitar erro.
                if (Papel == null)
                    Papel = new Papel();

                // Define o nome do papel informado.
                Papel.Nome = value;
            }
        }

        // Construtor vazio.
        // Pode ser utilizado quando os dados serão preenchidos depois.
        public Usuario() { }

        // Construtor utilizado para criar um usuário já com seus dados principais.
        public Usuario(string? nome, string? login, string? senhaHash, int papelId)
        {
            Nome = nome;
            Login = login;
            SenhaHash = senhaHash;
            PapelId = papelId;

            // Todo usuário novo inicia ativo
            Ativo = true;
        }

        // Verifica se o usuário possui o papel de Administrador.
        // Retorna true se for administrador e false caso contrário.
        public bool IsAdmin()
            => Papel?.Nome == Papel.ADMIN;

        // Verifica se o usuário possui o papel de Operador.
        public bool IsOperador()
            => Papel?.Nome == Papel.OPERADOR;

        // Verifica se o usuário possui o papel de Visualizador.
        public bool IsVisualizador()
            => Papel?.Nome == Papel.VISUALIZADOR;
    }
}