// ============================================================
// Models/Cliente.cs
// ============================================================
//
// O QUE É UM MODEL?
// Model é uma classe que representa uma "coisa" do mundo real
// dentro do nosso programa. No caso, um Cliente da barbearia.
//
// Pense nela como uma "ficha de papel" que tem os dados do cliente.
// A gente preenche os campos (nome, telefone) e guarda essa ficha.
//
// COMO FUNCIONA?
// - Cada "propriedade" (ex: public string Nome) é um campo da ficha
// - O construtor (public Cliente(...)) é o momento de preencher a ficha
// - ToString() é como o cliente aparece quando a gente imprime ele na tela

// "namespace" é como uma pasta organizadora do código.
// Tudo que está dentro de AgendaDeAtendimentos.Models vive nessa pasta.
// Isso evita que nomes iguais de classes se confundam.
namespace AgendaDeAtendimentos.Models
{
    // "public class Cliente" - Estou criando uma classe pública chamada Cliente.
    // "public" = qualquer parte do programa pode usar ela.
    // "class" = é uma classe (um molde para criar objetos).
    // "Cliente" = o nome dessa classe.
    public class Cliente
    {
        // --- PROPRIEDADES (OS CAMPOS DA FICHA) ---

        // Id: número único que identifica cada cliente.
        // "int" = número inteiro (1, 2, 3...)
        // "{ get; set; }" = pode escrever e ler esse valor.
        public int Id { get; set; }

        // Nome: o nome do cliente.
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

        // Construtor VAZIO (sem parâmetros).
        // "public Cliente()" - esse método especial roda quando a gente faz "new Cliente()"
        // Ele serve para criar um cliente vazio e preencher os dados depois.
        // As chaves {} estão vazias porque não precisamos fazer nada na criação.
        public Cliente() { }

        // Construtor COM parâmetros (mais prático).
        // Quando a gente faz "new Cliente("João", "9999-0000")", já preenche tudo de uma vez.
        // "string nome" = a gente recebe o nome como argumento
        // "string telefone" = a gente recebe o telefone como argumento
        // "string email = \"\"" = email é opcional, se não passar, fica vazio
        public Cliente(string nome, string telefone, string email = "")
        {
            // "this.Nome" = a propriedade Nome da classe (lá em cima)
            // "nome" = o valor que chegou no parâmetro
            // Estamos copiando o valor recebido para dentro da propriedade.
            Nome = nome;
            Telefone = telefone;
            Email = email;
        }


        // --- MÉTODOS ---

        // ToString() é um método especial que toda classe tem.
        // Ele decide como o objeto vira texto quando a gente exibe ele.
        // "override" = estou sobrescrevendo (substituindo) o comportamento padrão.
        // "$" = interpolação de string (permite colocar variáveis dentro do texto)
        // Exemplo: se Nome = "João" e Telefone = "9999-0000", mostra "João - 9999-0000"
        public override string ToString() => $"{Nome} - {Telefone}";
    }
}
