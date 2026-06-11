
// Molde para criar serviços

//
// Um serviço é algo que a barbearia oferece,
// como "Corte de Cabelo" ou "Barba".

public class Servico
{
    // Propriedades

    // Guarda o nome do serviço, tipo "Corte de Cabelo"
    public string Nome { get; set; }

    // "decimal" é um tipo numérico para valores com vírgula (dinheiro)
    // É mais preciso que "double" para dinheiro — evita erros de arredondamento
    public decimal Preco { get; set; }

    // Construtor

    // Precisa receber nome e preço para criar um serviço
    // Exemplo: new Servico("Corte", 25.00m)
    // O "m" no final do número indica que é decimal
    public Servico(string nome, decimal preco)
    {
        // Guarda os valores recebidos dentro do objeto
        Nome = nome;
        Preco = preco;
    }

    // ToString

    // Define como o serviço aparece nas listas (ListBox, ComboBox, etc.)
    public override string ToString()
    {
        // ":F2" é uma máscara de formatação
        // F = fixed-point (ponto fixo), 2 = duas casas decimais
        // Exemplo: "Corte - R$25,00"
        return $"{Nome} - R${Preco:F2}";
    }
}
