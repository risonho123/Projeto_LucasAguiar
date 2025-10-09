namespace BarbeariaLucasAaguiar.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataAniversario { get; set; }
        public string? Rg { get; set; }
    }
}
