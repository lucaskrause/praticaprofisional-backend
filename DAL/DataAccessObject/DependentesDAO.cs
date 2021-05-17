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
                    string sql = @"";

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
                    string sql = @"";

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
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", dependente.nome);
                    command.Parameters.AddWithValue("@cpf", dependente.cpf);
                    command.Parameters.AddWithValue("@rg", dependente.rg);
                    command.Parameters.AddWithValue("@sexo", dependente.sexo);
                    command.Parameters.AddWithValue("@email", dependente.email);
                    command.Parameters.AddWithValue("@telefone", dependente.telefone);
                    command.Parameters.AddWithValue("@dtNascimento", dependente.dtNascimento);
                    command.Parameters.AddWithValue("@codigoCidade", dependente.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", dependente.logradouro);
                    command.Parameters.AddWithValue("@complemento", dependente.complemento);
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
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", dependente.nome);
                    command.Parameters.AddWithValue("@cpf", dependente.cpf);
                    command.Parameters.AddWithValue("@rg", dependente.rg);
                    command.Parameters.AddWithValue("@sexo", dependente.sexo);
                    command.Parameters.AddWithValue("@email", dependente.email);
                    command.Parameters.AddWithValue("@telefone", dependente.telefone);
                    command.Parameters.AddWithValue("@dtNascimento", dependente.dtNascimento);
                    command.Parameters.AddWithValue("@codigoCidade", dependente.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", dependente.logradouro);
                    command.Parameters.AddWithValue("@complemento", dependente.complemento);
                    command.Parameters.AddWithValue("@bairro", dependente.bairro);
                    command.Parameters.AddWithValue("@cep", dependente.cep);
                    command.Parameters.AddWithValue("@codigoCliente", dependente.codigoCliente);
                    command.Parameters.AddWithValue("@dtAlteracao", dependente.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return dependente;
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
                    string sql = @"UPDATE dependentes SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM dependentes WHERE codigo = @codigo";

                    conexao.Open();

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
