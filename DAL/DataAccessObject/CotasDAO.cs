using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class CotasDAO : DAO<Cotas>
    {
        public override async Task<IList<Cotas>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Cotas> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cotas> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Cotas> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cotas> Inserir(Cotas cota)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoCliente", cota.codigoCliente);
                    command.Parameters.AddWithValue("@valor", cota.valor);
                    command.Parameters.AddWithValue("@dtInicio", cota.dtInicio);
                    command.Parameters.AddWithValue("@dtTermino", cota.dtTermino);
                    command.Parameters.AddWithValue("@codigoEmpresa", cota.codigoEmpresa);
                    command.Parameters.AddWithValue("@dtCadastro", cota.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", cota.dtAlteracao);
                    command.Parameters.AddWithValue("@status", cota.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    cota.codigo = (int)idInserido;
                    return cota;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cotas> Editar(Cotas cota)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoCliente", cota.codigoCliente);
                    command.Parameters.AddWithValue("@valor", cota.valor);
                    command.Parameters.AddWithValue("@dtInicio", cota.dtInicio);
                    command.Parameters.AddWithValue("@dtTermino", cota.dtTermino);
                    command.Parameters.AddWithValue("@codigoEmpresa", cota.codigoEmpresa);
                    command.Parameters.AddWithValue("@dtAlteracao", cota.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return cota;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Cotas cota)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE cotas SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM cotas WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", cota.status);
                    command.Parameters.AddWithValue("@dtAlteracao", cota.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", cota.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Cotas>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
