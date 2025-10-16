using LucasAguiar.Configs;

namespace LucasAguiar.Models
{
    public class ServicoDAO
    {
        private readonly Conexao _conexao;

        public ServicoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Servico servico)
        {
            try
            {
                var comando = _conexao.CreateCommand( @"INSERT INTO servico (nome_serv, preco_serv, duracao_min, comis_funcionario_cli) VALUES (@_nome, @_preco, @_duracao, @_comissao)
                ");

                comando.Parameters.AddWithValue("@_nome", servico.NomeServico);
                comando.Parameters.AddWithValue("@_preco", servico.PrecoServico);
                comando.Parameters.AddWithValue("@_duracao", servico.DuracaoServico);
                comando.Parameters.AddWithValue("@_comissao", servico.ComissaoServico);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir Serviço: "+ ex.Message);
            }
        }

        public List<Servico> ListarTodos()
        {
            var lista = new List<Servico>();

            var comando = _conexao.CreateCommand("SELECT * FROM servico");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var servico = new Servico
                {
                    IdServico =  leitor.GetInt32("id_serv"),
                    NomeServico = leitor.IsDBNull(leitor.GetOrdinal("nome_serv")) ? "" : leitor.GetString("nome_serv"),
                    PrecoServico = leitor.IsDBNull(leitor.GetOrdinal("preco_serv"))? 0f : leitor.GetFloat(leitor.GetOrdinal("preco_serv")),
                    DuracaoServico = leitor.GetInt32("duracao_min"),
                    ComissaoServico =  leitor.IsDBNull(leitor.GetOrdinal("comis_funcionario_cli"))? 0f : leitor.GetFloat(leitor.GetOrdinal("comis_funcionario_cli"))
            };

                lista.Add(servico);

            }
            return lista;
        }
    }
}