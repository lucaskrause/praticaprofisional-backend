using DAL.Entities;
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
                    string sql = @"SELECT clientes.codigo, clientes.nome, clientes.tipopessoa, clientes.cpfcnpj, clientes.rgie, clientes.sexo, clientes.email, clientes.telefone, clientes.dtnascfundacao, clientes.codigocidade, clientes.logradouro, clientes.complemento, clientes.bairro, clientes.cep, clientes.tipocliente, clientes.codigoformapagamento, clientes.dtcadastro, clientes.dtalteracao, clientes.status, cidades.cidade as nomeCidade, formaspagamento.descricao as nomeForma FROM clientes inner join cidades on (clientes.codigoCidade = cidades.codigo) inner join formaspagamento on (clientes.codigoformapagamento = formaspagamento.codigo) WHERE clientes.status = 'Ativo';";

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
                    string sql = @"SELECT clientes.codigo, clientes.nome, clientes.tipopessoa, clientes.cpfcnpj, clientes.rgie, clientes.sexo, clientes.email, clientes.telefone, clientes.dtnascfundacao, clientes.codigocidade, clientes.logradouro, clientes.complemento, clientes.bairro, clientes.cep, clientes.tipocliente, clientes.codigoformapagamento, clientes.dtcadastro, clientes.dtalteracao, clientes.status, cidades.cidade as nomeCidade, formaspagamento.descricao as nomeForma FROM clientes inner join cidades on (clientes.codigoCidade = cidades.codigo) inner join formaspagamento on (clientes.codigoformapagamento = formaspagamento.codigo) WHERE clientes.codigo = @codigo AND clientes.status = 'Ativo';";

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

        public override async Task<Clientes> Inserir(Clientes clientes)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"INSERT INTO clientes(nome, tipopessoa, cpfcnpj, rgie, sexo, email, telefone, dtnascfundacao, codigocidade, logradouro, complemento, bairro, cep, tipocliente, codigoformapagamento, dtcadastro, dtalteracao, status) VALUES (@nome, @tipoPessoa, @cpfcnpj, @rgie, @sexo, @email, @telefone, @dtnascfundacao, @codigoCidade, @logradouro, @complemento, @bairro, @cep, @tipoCliente, @codigoFormaPagamento, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", clientes.nome);
                    command.Parameters.AddWithValue("@tipoPessoa", clientes.tipoPessoa);
                    command.Parameters.AddWithValue("@cpfcnpj", clientes.cpfCnpj);
                    command.Parameters.AddWithValue("@rgie", clientes.rgIe);
                    command.Parameters.AddWithValue("@sexo", clientes.sexo);
                    command.Parameters.AddWithValue("@email", clientes.email);
                    command.Parameters.AddWithValue("@telefone", clientes.telefone);
                    command.Parameters.AddWithValue("@dtnascfundacao", clientes.dtNascFundacao);
                    command.Parameters.AddWithValue("@codigoCidade", clientes.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", clientes.logradouro);
                    command.Parameters.AddWithValue("@complemento", clientes.complemento);
                    command.Parameters.AddWithValue("@bairro", clientes.bairro);
                    command.Parameters.AddWithValue("@cep", clientes.cep);
                    command.Parameters.AddWithValue("@tipoCliente", clientes.tipoCliente);
                    command.Parameters.AddWithValue("@codigoFormaPagamento", clientes.codigoFormaPagamento);
                    command.Parameters.AddWithValue("@dtCadastro", clientes.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", clientes.dtAlteracao);
                    command.Parameters.AddWithValue("@status", clientes.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    clientes.codigo = (int)idInserido;
                    return clientes;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Clientes> Editar(Clientes clientes)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE clientes SET codigo = @codigo, nome = @nome, tipopessoa = @tipoPessoa, cpfcnpj = @cpfcnpj, rgie = @rgie, sexo = @sexo, email = @email, telefone = @telefone, dtnascfundacao = @dtNascFundacao, codigocidade = @codigoCidade, logradouro = @logradouro, complemento = @complemento, bairro = @bairro, cep = @cep, tipocliente = @tipoCliente, codigoformapagamento = @codigoFormaPagamento, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", clientes.nome);
                    command.Parameters.AddWithValue("@tipoPessoa", clientes.tipoPessoa);
                    command.Parameters.AddWithValue("@cpfcnpj", clientes.cpfCnpj);
                    command.Parameters.AddWithValue("@rgie", clientes.rgIe);
                    command.Parameters.AddWithValue("@sexo", clientes.sexo);
                    command.Parameters.AddWithValue("@email", clientes.email);
                    command.Parameters.AddWithValue("@telefone", clientes.telefone);
                    command.Parameters.AddWithValue("@dtnascfundacao", clientes.dtNascFundacao);
                    command.Parameters.AddWithValue("@codigoCidade", clientes.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", clientes.logradouro);
                    command.Parameters.AddWithValue("@complemento", clientes.complemento);
                    command.Parameters.AddWithValue("@bairro", clientes.bairro);
                    command.Parameters.AddWithValue("@cep", clientes.cep);
                    command.Parameters.AddWithValue("@tipoCliente", clientes.tipoCliente);
                    command.Parameters.AddWithValue("@codigoFormaPagamento", clientes.codigoFormaPagamento);
                    command.Parameters.AddWithValue("@dtAlteracao", clientes.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return clientes;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Clientes clientes)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE clientes SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM clientes WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", clientes.status);
                    command.Parameters.AddWithValue("@dtAlteracao", clientes.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", clientes.codigo);

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
