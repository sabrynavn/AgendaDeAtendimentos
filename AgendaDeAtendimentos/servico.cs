public class Servico
{
    public string Nome { get; set; } //Guarda o nome do serviço ,  tipo assim "corte de cabelo"

    // "decimal" é um tipo numérico usado para valores monetários
    // Mais preciso que "double" para dinheiro — evita erros de arredondamento
    public decimal Preco { get; set; }

    // Construtor — precisa receber nome e preço para criar um serviço
    public Servico(string nome, decimal preco)
    {
        Nome = nome;
        Preco = preco;
    }
    public override string ToString()
    {
        // ":F2" é uma máscara de formatação — garante sempre 2 casas decimais , vai ficar tipo R$ Barba R$25,00
        return $"{Nome} - R${Preco:F2}"; 
    }
}