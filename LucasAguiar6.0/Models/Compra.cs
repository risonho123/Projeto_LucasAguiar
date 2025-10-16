namespace LucasAguiar.Models
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public string? ValorCompra { get; set; }
        public string? ItemCompra { get; set; }
        public string? Quantidade { get; set; }
        public int IdProduto { get; set; }
        public int IdFornecedor { get; set; }
        public int IdFuncionario { get; set; }

        // Dados via JOIN
        public string? NomeProduto { get; set; }
        public string? NomeFornecedor { get; set; }
        public string? NomeFuncionario { get; set; }
    }
}
