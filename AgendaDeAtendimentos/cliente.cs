// Define um "molde" chamado Cliente
// Tudo que está dentro das chaves {} pertence a essa classe
public class Cliente
{
    // "Propriedade" Nome — é como uma variável que guarda o nome do cliente
    // "get" = permite ler o valor | "set" = permite alterar o valor
    public string Nome { get; set; }
    public string Telefone { get; set; }

    // Construtor — é um método especial que roda quando você cria um novo Cliente
    // Os parâmetros (nome, telefone) são as informações que você precisa passar na hora de criar
    // Exemplo de uso: new Cliente("João", "79 99999-0000")
    public clientes(string nome, string telefone)
    {
        //Aqui ele vai pegar o valor que veio do parâmetro "Nome e Telefone" e guardar dentro da propriedade "nome" e "telefone" do objeto
        //"this.nome" é como a propriedade "nome" or "telefone" desses objetos está sendo criada 
        Nome = nome;
        Telefone = telefone;
    }
    // Override = estamos "sobrescrevendo" um comportamento padrão do C#
    // ToString() é chamado automaticamente quando o C# precisa transformar
    // o objeto em texto — por exemplo, quando aparece em uma ListBox
    public override string ToString()
    
     // Retorna uma string formatada com $ na frente (interpolação de string)
        // {Nome} e {Telefone} são substituídos pelos valores reais na hora de rodar
        // Resultado exemplo: "João - 79 99999-0000"
        return $"{Nome} - {Telefone}";
    }
}