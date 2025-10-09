using BarbeariaLucasAaguiar.Configs;
using System.Data;

namespace BarbeariaLucasAaguiar.Models
{
    public class ClienteDAO
    {
        private readonly Conexao _conexao;
        public ClienteDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Adicionar(Cliente cliente)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                    "INSERT INTO cliente (nome_cli, telefone_cli, cpf_cli, data_nascimento_cli, rg_cli) " +
                    "VALUES (@_nome, @_telefone, @_cpf, @_data, @_rg)");

                comando.Parameters.AddWithValue("@_nome", cliente.Nome);
                comando.Parameters.AddWithValue("@_telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@_cpf", cliente.Cpf);
                comando.Parameters.AddWithValue("@_data", cliente.DataAniversario);
                comando.Parameters.AddWithValue("@_rg", cliente.Rg);

                comando.ExecuteNonQuery(); //  Isso executa o INSERT
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar cliente: " + ex.Message);
            }
        }

        public List<Cliente> ListarTodos()
        {
            var lista = new List<Cliente>();
            var comando = _conexao.CreateCommand("SELECT * FROM cliente");
            var leitor = comando.ExecuteReader();
            while (leitor.Read())
            {
                lista.Add(new Cliente
                {
                    Id = leitor.GetInt32("id_cli"),
                    Nome = leitor.GetString("nome_cli"),
                    Telefone = leitor.GetString("telefone_cli"),
                    Cpf = leitor.GetString("cpf_cli"),
                    DataAniversario = leitor.GetDateTime("data_nascimento_cli"),
                    Rg = leitor.GetString("rg_cli")

                });
            }
            return lista;
        }
    }
}
