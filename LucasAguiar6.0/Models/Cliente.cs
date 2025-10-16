
namespace LucasAguiar.Models

{
    public class Cliente
    {
        public int IdCliente{ get; set; }
        public string? NomeCliente { get; set; }
        public string? Telefone { get; set; }
        public string? CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? RG { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Bairro { get; set; }
        public string? Rua { get; set; }
        public string? Numero { get; set; }

        // join 

        public int? IdPlano { get; set; }
        public string? NomeFornecedor { get; set; }
    }
}


