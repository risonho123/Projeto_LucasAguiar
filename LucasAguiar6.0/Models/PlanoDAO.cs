using LucasAguiar.Configs;

namespace LucasAguiar.Models
{
    public class PlanoDAO
    {
        private readonly Conexao _conexao;

        public PlanoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Plano plano)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"INSERT INTO plano (nome_plan, descricao_plan, valor_plan)
                    VALUES (@_nome, @_descricao, @_valor)
                ");

                comando.Parameters.AddWithValue("@_nome", plano.NomePlano);
                comando.Parameters.AddWithValue("@_descricao", plano.Descricao);
                comando.Parameters.AddWithValue("@_valor", plano.Valor);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir fornecedor: " + ex.Message);
            }
        }

        public List<Plano> ListarTodos()
        {
            var lista = new List<Plano>();

            var comando = _conexao.CreateCommand("SELECT * FROM plano");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var plano = new Plano
                    {
                        IdPlano = leitor.GetInt32("id_plan"),
                        NomePlano = leitor.IsDBNull(leitor.GetOrdinal("nome_plan")) ? "" : leitor.GetString("nome_plan"),
                        Descricao = leitor.IsDBNull(leitor.GetOrdinal("descricao_plan")) ? "" : leitor.GetString("descricao_plan"),
                        Valor = leitor.IsDBNull(leitor.GetOrdinal("valor_plan")) ? 0f : leitor.GetFloat(leitor.GetOrdinal("valor_plan"))
                    };


                lista.Add(plano);
            }

            return lista;
        }
    }
}
