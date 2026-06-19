namespace AgendaDeAtendimentos.Models
{

    public class Cliente
    {
        // --- PROPRIEDADES (OS CAMPOS DA FICHA) ---

        // "int" = número inteiro (1, 2, 3...)
        // "{ get; set; }" = pode escrever e ler esse valor.
        public int Id { get; set; }

        // "string" = texto (palavras, frases)
        // "= \"\"" = começa com texto vazio para não dar erro.
        public string Nome { get; set; } = "";

        // Telefone: o telefone para contato.
        // Também é texto porque telefone tem parênteses, traços, etc.
        public string Telefone { get; set; } = "";

        // Email: o email do cliente (adicionado para combinar com o banco de dados).
        // O banco tem uma coluna "email", então precisamos guardar isso aqui também.
        public string Email { get; set; } = "";


        // --- CONSTRUTORES ---

        public Cliente() { }

        public Cliente(string nome, string telefone, string email = "")
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
        }


        public override string ToString() => $"{Nome} - {Telefone}";
    }
}
