
//  Molde para criar clientes

// Uma classe funciona como um "molde" ou "receita".
// Com ela podemos criar quantos clientes quisermos,
// cada um com seu próprio nome e telefone.

public class Cliente
{
    // Propriedades (características do cliente)


    // "get;" = permite LER o valor (ex: cliente.Nome)
    // "set;" = permite ESCREVER o valor (ex: cliente.Nome = "João")
    public string Nome { get; set; }
    public string Telefone { get; set; }

    // Construtor 

    // O construtor tem o MESMO nome da classe (Cliente)
    // Ele é chamado quando usamos "new Cliente(...)"
    // Os parâmetros (nome, telefone) são obrigatórios
    // Exemplo de uso: new Cliente("João", "79 99999-0000")
    public Cliente(string nome, string telefone)
    {
        // "this.Nome" se refere à propriedade Nome lá de cima
        // "nome" (sem this) é o parâmetro que veio do construtor
        // Estamos guardando o valor recebido dentro do objeto
        Nome = nome;
        Telefone = telefone;
    }

    //ToString (como o cliente aparece nas listas)

    // "override" = estamos SOBRESCREVENDO um comportamento padrão do C#
    // O método ToString() existe em TODOS os objetos do C#
    // Por padrão ele mostra o nome da classe, mas queremos mostrar algo útil
    
    // O C# chama ToString() automaticamente quando precisa transformar
    // o objeto em texto — por exemplo, quando aparece em uma ListBox
    public override string ToString()
    {
        return $"{Nome} - {Telefone}";
    }
}
