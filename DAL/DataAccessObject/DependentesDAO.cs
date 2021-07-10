using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class DependentesDAO : DAO<Dependentes>
    {
        public override async Task<IList<Dependentes>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT dependentes.codigo, dependentes.nome, dependentes.cpf, dependentes.rg, dependentes.sexo, dependentes.email, dependentes.telefone, dependentes.dtnascimento, dependentes.codigocidade, dependentes.logradouro, dependentes.complemento, dependentes.bairro, dependentes.cep, dependentes.codigocliente, dependentes.dtcadastro, dependentes.dtalteracao, dependentes.status, cidades.cidade as nomeCidade, clientes.nome as nomeCliente FROM dependentes INNER JOIN cidades ON (dependentes.codigoCidade = cidades.codigo) INNER JOIN clientes ON (dependentes.codigoCliente = clientes.codigo) WHERE dependentes.status = 'Ativo' ORDER BY dependentes.codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Dependentes> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Dependentes> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT dependentes.codigo, dependentes.nome, dependentes.cpf, dependentes.rg, dependentes.sexo, dependentes.email, dependentes.telefone, dependentes.dtnascimento, dependentes.codigocidade, dependentes.logradouro, dependentes.complemento, dependentes.bairro, dependentes.cep, dependentes.codigocliente, dependentes.dtcadastro, dependentes.dtalteracao, dependentes.status, cidades.cidade as nomeCidade, clientes.nome as nomeCliente FROM dependentes INNER JOIN cidades ON (dependentes.codigoCidade = cidades.codigo) INNER JOIN clientes ON (dependentes.codigoCliente = clientes.codigo) WHERE dependentes.codigo = @codigo AND dependentes.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Dependentes> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Dependentes> Inserir(Dependentes dependente)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "dependentes", "cpf", dependente.cpf);
                    if (exists)
                    {
                        string sql = @"INSERT INTO dependentes(nome, cpf, rg, sexo, email, telefone, dtnascimento, codigocidade, logradouro, complemento, bairro, cep, codigocliente, dtcadastro, dtalteracao, status) VALUES (@nome, @cpf, @rg, @sexo, @email, @telefone, @dtNascimento, @codigoCidade, @logradouro, @complemento, @bairro, @cep, @codigoCliente, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@nome", dependente.nome);
                        command.Parameters.AddWithValue("@cpf", dependente.cpf);
                        command.Parameters.AddWithValue("@rg", dependente.rg ?? (Object)DBNull.Value);
                        command.Parameters.AddWithValue("@sexo", dependente.sexo);
                        command.Parameters.AddWithValue("@email", dependente.email);
                        command.Parameters.AddWithValue("@telefone", dependente.telefone);
                        command.Parameters.AddWithValue("@dtNascimento", dependente.dtNascimento);
                        command.Parameters.AddWithValue("@codigoCidade", dependente.codigoCidade);
                        command.Parameters.AddWithValue("@logradouro", dependente.logradouro);
                        command.Parameters.AddWithValue("@complemento", dependente.complemento ?? (Object)DBNull.Value);
                        command.Parameters.AddWithValue("@bairro", dependente.bairro);
                        command.Parameters.AddWithValue("@cep", dependente.cep);
                        command.Parameters.AddWithValue("@codigoCliente", dependente.codigoCliente);
                        command.Parameters.AddWithValue("@dtCadastro", dependente.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", dependente.dtAlteracao);
                        command.Parameters.AddWithValue("@status", dependente.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        dependente.codigo = (int)idInserido;
                        return dependente;
                    }
                    else
                    {
                        throw new Exception("Dependente já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Dependentes> Editar(Dependentes dependente)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "dependentes", "cpf", dependente.cpf, dependente.codigo);
                    if (exists)
                    {
                        string sql = @"UPDATE dependentes SET nome = @nome, cpf = @cpf, rg = @rg, sexo = @sexo, email = @email, telefone = @telefone, dtnascimento = @dtNascimento, codigocidade = @codigoCidade, logradouro = @logradouro, complemento = @complemento, bairro = @bairro, cep = @cep, codigocliente = @codigoCliente, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@nome", dependente.nome);
                        command.Parameters.AddWithValue("@cpf", dependente.cpf);
                        command.Parameters.AddWithValue("@rg", dependente.rg ?? (Object)DBNull.Value);
                        command.Parameters.AddWithValue("@sexo", dependente.sexo);
                        command.Parameters.AddWithValue("@email", dependente.email);
                        command.Parameters.AddWithValue("@telefone", dependente.telefone);
                        command.Parameters.AddWithValue("@dtNascimento", dependente.dtNascimento);
                        command.Parameters.AddWithValue("@codigoCidade", dependente.codigoCidade);
                        command.Parameters.AddWithValue("@logradouro", dependente.logradouro);
                        command.Parameters.AddWithValue("@complemento", dependente.complemento ?? (Object)DBNull.Value);
                        command.Parameters.AddWithValue("@bairro", dependente.bairro);
                        command.Parameters.AddWithValue("@cep", dependente.cep);
                        command.Parameters.AddWithValue("@codigoCliente", dependente.codigoCliente);
                        command.Parameters.AddWithValue("@dtAlteracao", dependente.dtAlteracao);
                        command.Parameters.AddWithValue("@codigo", dependente.codigo);

                        await command.ExecuteNonQueryAsync();
                        return dependente;
                    }
                    else
                    {
                        throw new Exception("Dependente já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Dependentes dependente)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM dependentes WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", dependente.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE dependentes SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", dependente.status);
                    command.Parameters.AddWithValue("@dtAlteracao", dependente.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", dependente.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Dependentes>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
