using LucasAguiar.Configs;

namespace LucasAguiar.Models
{
    public class ClienteDAO
    {
        private readonly Conexao _conexao;

        public ClienteDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Cliente cliente)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                        INSERT INTO cliente (nome_cli, telefone_cli, cpf_cli, data_nasc_cli, rg_cli, estado_cli, cidade_cli, bairro_cli, rua_cli, numero_cli)
                        VALUES (@_nome, @_telefone, @_cpf, @_dataNasc, @_rg, @_estado, @_cidade, @_bairro, @_rua, @_numero)
                ");     
                comando.Parameters.AddWithValue("@_nome", cliente.NomeCliente);
                comando.Parameters.AddWithValue("@_telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@_cpf", cliente.CPF);
                comando.Parameters.AddWithValue("@_dataNasc", cliente.DataNascimento);
                comando.Parameters.AddWithValue("@_rg", cliente.RG);
                comando.Parameters.AddWithValue("@_estado", cliente.Estado);
                comando.Parameters.AddWithValue("@_cidade", cliente.Cidade);
                comando.Parameters.AddWithValue("@_bairro", cliente.Bairro);
                comando.Parameters.AddWithValue("@_rua", cliente.Rua);
                comando.Parameters.AddWithValue("@_numero", cliente.Numero);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir cliente: " + ex.Message);
            }
        }

     public List<Cliente> ListarTodos()
{
    var lista = new List<Cliente>();
    var comando = _conexao.CreateCommand("SELECT * FROM cliente");
    var leitor = comando.ExecuteReader();

    while (leitor.Read())
    {
        var cliente = new Cliente
        {
            IdCliente = leitor.GetInt32("id_cli"),
            NomeCliente = leitor.IsDBNull(leitor.GetOrdinal("nome_cli")) ? null : leitor.GetString("nome_cli"),
            Telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone_cli")) ? null : leitor.GetString("telefone_cli"),
            CPF = leitor.IsDBNull(leitor.GetOrdinal("cpf_cli")) ? null : leitor.GetString("cpf_cli"),
            DataNascimento = leitor.IsDBNull(leitor.GetOrdinal("data_nasc_cli")) ? null : leitor.GetDateTime("data_nasc_cli"),
            RG = leitor.IsDBNull(leitor.GetOrdinal("rg_cli")) ? null : leitor.GetString("rg_cli"),
            Estado = leitor.IsDBNull(leitor.GetOrdinal("estado_cli")) ? null : leitor.GetString("estado_cli"),
            Cidade = leitor.IsDBNull(leitor.GetOrdinal("cidade_cli")) ? null : leitor.GetString("cidade_cli"),
            Bairro = leitor.IsDBNull(leitor.GetOrdinal("bairro_cli")) ? null : leitor.GetString("bairro_cli"),
            Rua = leitor.IsDBNull(leitor.GetOrdinal("rua_cli")) ? null : leitor.GetString("rua_cli"),
            Numero = leitor.IsDBNull(leitor.GetOrdinal("numero_cli")) ? null : leitor.GetString("numero_cli"),
        };

        lista.Add(cliente);
    }

    return lista;
}
    }
}
