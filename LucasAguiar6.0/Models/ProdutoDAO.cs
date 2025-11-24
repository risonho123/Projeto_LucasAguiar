using LucasAguiar.Configs;
using MySqlConnector;
using AppWeb.Configs;

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
         public Produto? BuscarPorId(int id)
        {
            var comando = _conexao.CreateCommand(
                "SELECT * FROM produto WHERE id_prod = @id;");
            comando.Parameters.AddWithValue("@id", id);

            var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                var produto = new Produto();
                produto.IdProduto = leitor.GetInt32("id_prod");
                produto.NomeProduto = DAOHelper.GetString(leitor, "nome_prod");
                produto.Descricao = DAOHelper.GetString(leitor, "descricao_prod");
                produto.Marca = DAOHelper.GetString(leitor, "marca_prod");
                produto.Quantidade = leitor.GetInt32("quantidade_prod");
                produto.Valor = leitor.GetFloat("valor_prod");

                return produto;
            }
            else
            {
                return null;
            }
        }
public void Atualizar(Produto produto)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                "UPDATE produto SET nome_prod = @_nome, descricao_prod = @_descricao, marca_prod = @_marca," +
                "quantidade_prod = @_quantidade, valor_prod = @_preco WHERE id_prod = @_id;");

                comando.Parameters.AddWithValue("@_nome", produto.NomeProduto);
                comando.Parameters.AddWithValue("@_descricao", produto.Descricao);
                comando.Parameters.AddWithValue("@_marca", produto.Marca);
                comando.Parameters.AddWithValue("@_quantidade", produto.Quantidade);
                comando.Parameters.AddWithValue("@_preco", produto.Valor);
                comando.Parameters.AddWithValue("@_id", produto.IdProduto);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                "DELETE FROM produto WHERE id_prod = @id;");

                comando.Parameters.AddWithValue("@id", id);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
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
        IdProduto = leitor.IsDBNull(leitor.GetOrdinal("id_prod")) 
            ? 0 : leitor.GetInt32("id_prod"),

        NomeProduto = leitor.IsDBNull(leitor.GetOrdinal("nome_prod")) 
            ? "" : leitor.GetString("nome_prod"),

        Quantidade = leitor.IsDBNull(leitor.GetOrdinal("quantidade_prod")) 
            ? 0 : leitor.GetInt32("quantidade_prod"),

        Valor = leitor.IsDBNull(leitor.GetOrdinal("valor_prod")) 
            ? 0 : leitor.GetFloat("valor_prod"),

        Descricao = leitor.IsDBNull(leitor.GetOrdinal("descricao_prod")) 
            ? "" : leitor.GetString("descricao_prod"),

        Marca = leitor.IsDBNull(leitor.GetOrdinal("marca_prod")) 
            ? "" : leitor.GetString("marca_prod"),

        IdFornecedor = leitor.IsDBNull(leitor.GetOrdinal("id_forn_fk")) 
            ? 0 : leitor.GetInt32("id_forn_fk"),

        NomeFornecedor = leitor.IsDBNull(leitor.GetOrdinal("nome_forn")) 
            ? "" : leitor.GetString("nome_forn")
    };

    lista.Add(produto);



    
    }

    return lista;

        }
        }
}
