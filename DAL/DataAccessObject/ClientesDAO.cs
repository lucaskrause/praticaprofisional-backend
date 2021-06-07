using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ClientesDAO : DAO<Clientes>
    {
        public override async Task<IList<Clientes>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT clientes.codigo, clientes.nome, clientes.tipopessoa, clientes.cpfcnpj, clientes.rgie, clientes.sexo, clientes.email, clientes.telefone, clientes.dtnascfundacao, clientes.codigocidade, clientes.logradouro, clientes.complemento, clientes.bairro, clientes.cep, clientes.codigocondicaopagamento, clientes.dtcadastro, clientes.dtalteracao, clientes.status, cidades.cidade as nomeCidade, condicoespagamento.descricao AS nomeCondicao FROM clientes INNER JOIN cidades ON (clientes.codigoCidade = cidades.codigo) INNER JOIN condicoespagamento ON (clientes.codigocondicaopagamento = condicoespagamento.codigo) WHERE clientes.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Clientes> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
        public override async Task<Clientes> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT clientes.codigo, clientes.nome, clientes.tipopessoa, clientes.cpfcnpj, clientes.rgie, clientes.sexo, clientes.email, clientes.telefone, clientes.dtnascfundacao, clientes.codigocidade, clientes.logradouro, clientes.complemento, clientes.bairro, clientes.cep, clientes.codigocondicaopagamento, clientes.dtcadastro, clientes.dtalteracao, clientes.status, cidades.cidade as nomeCidade, condicoespagamento.descricao AS nomeCondicao FROM clientes INNER JOIN cidades ON (clientes.codigoCidade = cidades.codigo) INNER JOIN condicoespagamento ON (clientes.codigocondicaopagamento = condicoespagamento.codigo) WHERE clientes.codigo = @codigo AND clientes.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Clientes> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Clientes> Inserir(Clientes cliente)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"INSERT INTO clientes(nome, tipopessoa, cpfcnpj, rgie, sexo, email, telefone, dtnascfundacao, codigocidade, logradouro, complemento, bairro, cep, codigocondicaopagamento, dtcadastro, dtalteracao, status) VALUES (@nome, @tipoPessoa, @cpfcnpj, @rgie, @sexo, @email, @telefone, @dtnascfundacao, @codigoCidade, @logradouro, @complemento, @bairro, @cep, @codigoCondicaoPagamento, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", cliente.nome);
                    command.Parameters.AddWithValue("@tipoPessoa", cliente.tipoPessoa);
                    command.Parameters.AddWithValue("@cpfcnpj", cliente.cpfCnpj);
                    command.Parameters.AddWithValue("@rgie", cliente.rgIe);
                    command.Parameters.AddWithValue("@sexo", cliente.sexo);
                    command.Parameters.AddWithValue("@email", cliente.email);
                    command.Parameters.AddWithValue("@telefone", cliente.telefone);
                    command.Parameters.AddWithValue("@dtnascfundacao", cliente.dtNascimento);
                    command.Parameters.AddWithValue("@codigoCidade", cliente.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", cliente.logradouro);
                    command.Parameters.AddWithValue("@complemento", cliente.complemento);
                    command.Parameters.AddWithValue("@bairro", cliente.bairro);
                    command.Parameters.AddWithValue("@cep", cliente.cep);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", cliente.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtCadastro", cliente.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", cliente.dtAlteracao);
                    command.Parameters.AddWithValue("@status", cliente.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    cliente.codigo = (int)idInserido;
                    return cliente;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Clientes> Editar(Clientes cliente)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE clientes SET codigo = @codigo, nome = @nome, tipopessoa = @tipoPessoa, cpfcnpj = @cpfcnpj, rgie = @rgie, sexo = @sexo, email = @email, telefone = @telefone, dtnascfundacao = @dtNascFundacao, codigocidade = @codigoCidade, logradouro = @logradouro, complemento = @complemento, bairro = @bairro, cep = @cep, codigocondicaopagamento = @codigoCondicaoPagamento, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", cliente.nome);
                    command.Parameters.AddWithValue("@tipoPessoa", cliente.tipoPessoa);
                    command.Parameters.AddWithValue("@cpfcnpj", cliente.cpfCnpj);
                    command.Parameters.AddWithValue("@rgie", cliente.rgIe);
                    command.Parameters.AddWithValue("@sexo", cliente.sexo);
                    command.Parameters.AddWithValue("@email", cliente.email);
                    command.Parameters.AddWithValue("@telefone", cliente.telefone);
                    command.Parameters.AddWithValue("@dtnascfundacao", cliente.dtNascimento);
                    command.Parameters.AddWithValue("@codigoCidade", cliente.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", cliente.logradouro);
                    command.Parameters.AddWithValue("@complemento", cliente.complemento);
                    command.Parameters.AddWithValue("@bairro", cliente.bairro);
                    command.Parameters.AddWithValue("@cep", cliente.cep);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", cliente.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtAlteracao", cliente.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return cliente;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Clientes cliente)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE clientes SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM cliente WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", cliente.status);
                    command.Parameters.AddWithValue("@dtAlteracao", cliente.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", cliente.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Clientes>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
