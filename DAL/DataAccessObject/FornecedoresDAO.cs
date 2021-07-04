using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class FornecedoresDAO : DAO<Fornecedores>
    {
        public override async Task<IList<Fornecedores>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT fornecedores.codigo, fornecedores.nome, fornecedores.tipopessoa, fornecedores.cpfcnpj, fornecedores.rgie, fornecedores.sexo, fornecedores.email, fornecedores.telefone, fornecedores.dtnascfundacao, fornecedores.codigocidade, fornecedores.logradouro, fornecedores.complemento, fornecedores.bairro, fornecedores.cep, fornecedores.dtcadastro, fornecedores.dtalteracao, fornecedores.status, cidades.cidade as nomeCidade FROM fornecedores INNER JOIN cidades ON (fornecedores.codigoCidade = cidades.codigo) WHERE fornecedores.status = 'Ativo' ORDER BY fornecedores.codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Fornecedores> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
        public override async Task<Fornecedores> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT fornecedores.codigo, fornecedores.nome, fornecedores.tipopessoa, fornecedores.cpfcnpj, fornecedores.rgie, fornecedores.sexo, fornecedores.email, fornecedores.telefone, fornecedores.dtnascfundacao, fornecedores.codigocidade, fornecedores.logradouro, fornecedores.complemento, fornecedores.bairro, fornecedores.cep, fornecedores.dtcadastro, fornecedores.dtalteracao, fornecedores.status, cidades.cidade as nomeCidade FROM fornecedores INNER JOIN cidades ON (fornecedores.codigoCidade = cidades.codigo) WHERE fornecedores.codigo = @codigo AND fornecedores.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Fornecedores> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Fornecedores> Inserir(Fornecedores fornecedor)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "fornecedores", "cpfcnpj", fornecedor.cpfCnpj);
                    if (exists)
                    {
                        string sql = @"INSERT INTO fornecedores (nome, tipopessoa, cpfcnpj, rgie, sexo, email, telefone, dtnascfundacao, codigocidade, logradouro, complemento, bairro, cep, dtcadastro, dtalteracao, status) VALUES (@nome, @tipoPessoa, @cpfcnpj, @rgie, @sexo, @email, @telefone, @dtnascfundacao, @codigoCidade, @logradouro, @complemento, @bairro, @cep, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@nome", fornecedor.nome);
                        command.Parameters.AddWithValue("@tipoPessoa", fornecedor.tipoPessoa);
                        command.Parameters.AddWithValue("@cpfcnpj", fornecedor.cpfCnpj);
                        command.Parameters.AddWithValue("@rgie", fornecedor.rgIe);
                        command.Parameters.AddWithValue("@sexo", fornecedor.sexo);
                        command.Parameters.AddWithValue("@email", fornecedor.email);
                        command.Parameters.AddWithValue("@telefone", fornecedor.telefone);
                        command.Parameters.AddWithValue("@dtnascfundacao", fornecedor.dtNascimento);
                        command.Parameters.AddWithValue("@codigoCidade", fornecedor.codigoCidade);
                        command.Parameters.AddWithValue("@logradouro", fornecedor.logradouro);
                        command.Parameters.AddWithValue("@complemento", fornecedor.complemento);
                        command.Parameters.AddWithValue("@bairro", fornecedor.bairro);
                        command.Parameters.AddWithValue("@cep", fornecedor.cep);
                        command.Parameters.AddWithValue("@dtCadastro", fornecedor.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", fornecedor.dtAlteracao);
                        command.Parameters.AddWithValue("@status", fornecedor.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        fornecedor.codigo = (int)idInserido;
                        return fornecedor;
                    }
                    else
                    {
                        throw new Exception("Fornecedor já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Fornecedores> Editar(Fornecedores fornecedor)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "fornecedores", "cpfcnpj", fornecedor.cpfCnpj, fornecedor.codigo);
                    if (exists)
                    {
                        string sql = @"UPDATE fornecedores SET nome = @nome, tipopessoa = @tipoPessoa, cpfcnpj = @cpfcnpj, rgie = @rgie, sexo = @sexo, email = @email, telefone = @telefone, dtnascfundacao = @dtNascFundacao, codigocidade = @codigoCidade, logradouro = @logradouro, complemento = @complemento, bairro = @bairro, cep = @cep, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                        conexao.Open();

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@nome", fornecedor.nome);
                        command.Parameters.AddWithValue("@tipoPessoa", fornecedor.tipoPessoa);
                        command.Parameters.AddWithValue("@cpfcnpj", fornecedor.cpfCnpj);
                        command.Parameters.AddWithValue("@rgie", fornecedor.rgIe);
                        command.Parameters.AddWithValue("@sexo", fornecedor.sexo);
                        command.Parameters.AddWithValue("@email", fornecedor.email);
                        command.Parameters.AddWithValue("@telefone", fornecedor.telefone);
                        command.Parameters.AddWithValue("@dtnascfundacao", fornecedor.dtNascimento);
                        command.Parameters.AddWithValue("@codigoCidade", fornecedor.codigoCidade);
                        command.Parameters.AddWithValue("@logradouro", fornecedor.logradouro);
                        command.Parameters.AddWithValue("@complemento", fornecedor.complemento);
                        command.Parameters.AddWithValue("@bairro", fornecedor.bairro);
                        command.Parameters.AddWithValue("@cep", fornecedor.cep);
                        command.Parameters.AddWithValue("@dtAlteracao", fornecedor.dtAlteracao);

                        await command.ExecuteNonQueryAsync();
                        return fornecedor;
                    }
                    else
                    {
                        throw new Exception("Fornecedor já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Fornecedores fornecedor)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM fornecedores WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", fornecedor.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE fornecedores SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", fornecedor.status);
                    command.Parameters.AddWithValue("@dtAlteracao", fornecedor.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", fornecedor.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Fornecedores>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
