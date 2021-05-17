using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ContasBancariasDAO : DAO<ContasBancarias>
    {
        public override async Task<IList<ContasBancarias>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<ContasBancarias> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<ContasBancarias> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<ContasBancarias> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<ContasBancarias> Inserir(ContasBancarias contaBancaria)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoEmpresa", contaBancaria.codigoEmpresa);
                    command.Parameters.AddWithValue("@banco", contaBancaria.banco);
                    command.Parameters.AddWithValue("@numeroBanco", contaBancaria.numeroBanco);
                    command.Parameters.AddWithValue("@agencia", contaBancaria.agencia);
                    command.Parameters.AddWithValue("@conta", contaBancaria.conta);
                    command.Parameters.AddWithValue("@saldo", contaBancaria.saldo);
                    command.Parameters.AddWithValue("@dtCadastro", contaBancaria.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", contaBancaria.dtAlteracao);
                    command.Parameters.AddWithValue("@status", contaBancaria.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    contaBancaria.codigo = (int)idInserido;
                    return contaBancaria;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<ContasBancarias> Editar(ContasBancarias contaBancaria)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@banco", contaBancaria.banco);
                    command.Parameters.AddWithValue("@numeroBanco", contaBancaria.numeroBanco);
                    command.Parameters.AddWithValue("@agencia", contaBancaria.agencia);
                    command.Parameters.AddWithValue("@conta", contaBancaria.conta);
                    command.Parameters.AddWithValue("@saldo", contaBancaria.saldo);
                    command.Parameters.AddWithValue("@dtAlteracao", contaBancaria.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return contaBancaria;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(ContasBancarias contaBancaria)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE CREATE TABLE contasBancarias SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM contasBancarias WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", contaBancaria.status);
                    command.Parameters.AddWithValue("@dtAlteracao", contaBancaria.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", contaBancaria.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<ContasBancarias>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
