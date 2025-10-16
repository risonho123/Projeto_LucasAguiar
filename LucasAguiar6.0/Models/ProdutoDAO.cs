using LucasAguiar.Configs;

namespace LucasAguiar.Models
{
    public class ProdutoDAO
    {
        private readonly Conexao _conexao;

        public ProdutoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Produto produto)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                    INSERT INTO produto (nome_prod, quantidade_prod, valor_prod, descricao_prod, marca_prod, id_forn_fk)
                    VALUES (@_nome, @_quantidade, @_valor, @_descricao, @_marca, @_idForn)
                ");

                comando.Parameters.AddWithValue("@_nome", produto.NomeProduto);
                comando.Parameters.AddWithValue("@_quantidade", produto.Quantidade);
                comando.Parameters.AddWithValue("@_valor", produto.Valor);
                comando.Parameters.AddWithValue("@_descricao", produto.Descricao);
                comando.Parameters.AddWithValue("@_marca", produto.Marca);
                comando.Parameters.AddWithValue("@_idForn", produto.IdFornecedor);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir produto: " + ex.Message);
            }
        }

        public List<Produto> ListarTodos()
        {
            var lista = new List<Produto>();
            var comando = _conexao.CreateCommand(@"
                SELECT p.*, f.nome_forn
                FROM produto p
                INNER JOIN fornecedor f ON p.id_forn_fk = f.id_forn
            ");

            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var produto = new Produto
                {
                    IdProduto = leitor.GetInt32("id_prod"),
                    NomeProduto = leitor.GetString("nome_prod"),
                    Quantidade = leitor.GetInt32("quantidade_prod"),
                    Valor = leitor.GetFloat("valor_prod"),
                    Descricao = leitor.GetString("descricao_prod"),
                    Marca = leitor.GetString("marca_prod"),
                    IdFornecedor = leitor.GetInt32("id_forn_fk"),
                    NomeFornecedor = leitor.GetString("nome_forn")
                };

                lista.Add(produto);
            }

            return lista;
        }
    }
}
