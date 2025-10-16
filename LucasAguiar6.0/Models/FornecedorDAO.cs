using LucasAguiar.Configs;

namespace LucasAguiar.Models
{
    public class FornecedorDAO
    {
        private readonly Conexao _conexao;

        public FornecedorDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Fornecedor fornecedor)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"INSERT INTO fornecedor (nome_forn, email_forn, telefone_forn, tipo_prod_forn)
                    VALUES (@_nome, @_email, @_telefone, @_tipo)
                ");

                comando.Parameters.AddWithValue("@_nome", fornecedor.NomeFornecedor);
                comando.Parameters.AddWithValue("@_email", fornecedor.Email);
                comando.Parameters.AddWithValue("@_telefone", fornecedor.Telefone);
                comando.Parameters.AddWithValue("@_tipo", fornecedor.TipoProd);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir fornecedor: " + ex.Message);
            }
        }

        public List<Fornecedor> ListarTodos()
        {
            var lista = new List<Fornecedor>();

            var comando = _conexao.CreateCommand("SELECT * FROM fornecedor");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var fornecedor = new Fornecedor
                    {
                        IdFornecedor = leitor.GetInt32("id_forn"),
                        NomeFornecedor = leitor.IsDBNull(leitor.GetOrdinal("nome_forn")) ? "" : leitor.GetString("nome_forn"),
                        Email = leitor.IsDBNull(leitor.GetOrdinal("email_forn")) ? "" : leitor.GetString("email_forn"),
                        Telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone_forn")) ? "" : leitor.GetString("telefone_forn"),
                        TipoProd = leitor.IsDBNull(leitor.GetOrdinal("tipo_prod_forn")) ? "" : leitor.GetString("tipo_prod_forn")
                    };


                lista.Add(fornecedor);
            }

            return lista;
        }
    }
}
