using LucasAguiar.Configs;

namespace LucasAguiar.Models
{
    public class FuncionarioDAO
    {
        private readonly Conexao _conexao;

        public FuncionarioDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Funcionario funcionario)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                        INSERT INTO funcionario (nome_fun, telefone_fun, cpf_fun, data_nasc_fun, ctps_fun, rg_fun, estado_fun, cidade_fun, bairro_fun, rua_fun, numero_fun)
                        VALUES (@_nome, @_telefone, @_cpf, @_dataNasc,@_ctps, @_rg, @_estado, @_cidade, @_bairro, @_rua, @_numero)
                ");     
                comando.Parameters.AddWithValue("@_nome", funcionario.NomeFuncionario);
                comando.Parameters.AddWithValue("@_telefone", funcionario.Telefone);
                comando.Parameters.AddWithValue("@_cpf", funcionario.CPF);
                comando.Parameters.AddWithValue("@_dataNasc", funcionario.DataNascimento);
               comando.Parameters.AddWithValue("@_ctps", funcionario.CPF);
                comando.Parameters.AddWithValue("@_rg", funcionario.RG);
                comando.Parameters.AddWithValue("@_estado", funcionario.Estado);
                comando.Parameters.AddWithValue("@_cidade", funcionario.Cidade);
                comando.Parameters.AddWithValue("@_bairro", funcionario.Bairro);
                comando.Parameters.AddWithValue("@_rua", funcionario.Rua);
                comando.Parameters.AddWithValue("@_numero", funcionario.Numero);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir funcionario: " + ex.Message);
            }
        }

     public List<Funcionario> ListarTodos()
{
    var lista = new List<Funcionario>();
    var comando = _conexao.CreateCommand("SELECT * FROM funcionario");
    var leitor = comando.ExecuteReader();

    while (leitor.Read())
    {
        var funcionario = new Funcionario
        {
            IdFuncionario = leitor.GetInt32("id_fun"),
            NomeFuncionario = leitor.IsDBNull(leitor.GetOrdinal("nome_fun")) ? null : leitor.GetString("nome_fun"),
            Telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone_fun")) ? null : leitor.GetString("telefone_fun"),
            CPF = leitor.IsDBNull(leitor.GetOrdinal("cpf_fun")) ? null : leitor.GetString("cpf_fun"),
            DataNascimento = leitor.IsDBNull(leitor.GetOrdinal("data_nasc_fun")) ? null : leitor.GetDateTime("data_nasc_fun"),
            CTPS = leitor.IsDBNull(leitor.GetOrdinal("ctps_fun")) ? null : leitor.GetString("ctps_fun"),
            RG = leitor.IsDBNull(leitor.GetOrdinal("rg_fun")) ? null : leitor.GetString("rg_fun"),
            Estado = leitor.IsDBNull(leitor.GetOrdinal("estado_fun")) ? null : leitor.GetString("estado_fun"),
            Cidade = leitor.IsDBNull(leitor.GetOrdinal("cidade_fun")) ? null : leitor.GetString("cidade_fun"),
            Bairro = leitor.IsDBNull(leitor.GetOrdinal("bairro_fun")) ? null : leitor.GetString("bairro_fun"),
            Rua = leitor.IsDBNull(leitor.GetOrdinal("rua_fun")) ? null : leitor.GetString("rua_fun"),
            Numero = leitor.IsDBNull(leitor.GetOrdinal("numero_fun")) ? null : leitor.GetString("numero_fun"),
        };

        lista.Add(funcionario);
    }

    return lista;
}
    }
}
