namespace AgendaDeAtendimentos.Models
{
    public class Servico
    {
        public int Id { get; set; }

        public string Nome { get; set; } = "";

        // "decimal" = tipo próprio para dinheiro (não pode usar double, perde precisão)
        public decimal Valor { get; set; }

        // Construtor vazio - cria um serviço sem dados (preenche depois).
        public Servico() { }

        // Construtor que já recebe nome e valor na criação.
        // Ex: new Servico("Corte", 35.00m), o "m" depois do número indica que é um decimal literal.
        public Servico(string nome, decimal valor)
        {
            // Copia os valores recebidos para as propriedades da classe.
            Nome = nome;
            Valor = valor;
        }

        // ToString() formata como o serviço aparece nas listas da tela
        public override string ToString() => $"{Nome} - R${Valor:F2}";
    }
}
