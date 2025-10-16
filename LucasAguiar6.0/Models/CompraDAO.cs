using LucasAguiar.Configs;

namespace LucasAguiar.Models
{
    public class CompraDAO
    {
        private readonly Conexao _conexao;

        public CompraDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Compra compra)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                    INSERT INTO compras (data_comp, valor_comp, item_comp, quantidade, id_prod_fk, id_forn_fk, id_fun_fk)
                    VALUES (@_data, @_valor, @_item, @_quantidade, @_idProd, @_idForn, @_idFunc)
                ");

                comando.Parameters.AddWithValue("@_data", compra.DataCompra);
                comando.Parameters.AddWithValue("@_valor", compra.ValorCompra);
                comando.Parameters.AddWithValue("@_item", compra.ItemCompra);
                comando.Parameters.AddWithValue("@_quantidade", compra.Quantidade);
                comando.Parameters.AddWithValue("@_idProd", compra.IdProduto);
                comando.Parameters.AddWithValue("@_idForn", compra.IdFornecedor);
                comando.Parameters.AddWithValue("@_idFunc", compra.IdFuncionario);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir compra: " + ex.Message);
            }
        }

        public List<Compra> ListarTodos()
        {
            var lista = new List<Compra>();
            var comando = _conexao.CreateCommand(@"
                SELECT c.*, p.nome_prod, f.nome_forn, u.nome_fun
                FROM compras c
                INNER JOIN produto p ON c.id_prod_fk = p.id_prod
                INNER JOIN fornecedor f ON c.id_forn_fk = f.id_forn
                INNER JOIN funcionario u ON c.id_fun_fk = u.id_fun
            ");

            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var compra = new Compra
                {
                    IdCompra = leitor.GetInt32("id_comp"),
                    DataCompra = leitor.GetDateTime("data_comp"),
                    ValorCompra = leitor.GetString("valor_comp"),
                    ItemCompra = leitor.GetString("item_comp"),
                    Quantidade = leitor.GetString("quantidade"),
                    IdProduto = leitor.GetInt32("id_prod_fk"),
                    IdFornecedor = leitor.GetInt32("id_forn_fk"),
                    IdFuncionario = leitor.GetInt32("id_fun_fk"),
                    NomeProduto = leitor.GetString("nome_prod"),
                    NomeFornecedor = leitor.GetString("nome_forn"),
                    NomeFuncionario = leitor.GetString("nome_fun")
                };

                lista.Add(compra);
            }

            return lista;
        }
    }
}
