using Npgsql;
using RUPsystem.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class CidadesDAO : DAO<Cidades>
    {
        public CidadesDAO() : base()
        {
        }

        public override async Task<IList<Cidades>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM cidades WHERE status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    IList<Cidades> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cidades> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM cidades WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    IList<Cidades> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cidades> Inserir(Cidades cidade)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"INSERT INTO cidades (cidade, ddd, codigoEstado, dtCadastro, dtAlteracao, status) VALUES(@cidade, @ddd, @codigoEstado, @dtCadastro, @dtAlteracao, @status);";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@cidade", cidade.Cidade);
                    command.Parameters.AddWithValue("@ddd", cidade.ddd);
                    command.Parameters.AddWithValue("@codigoEstado", cidade.CodigoEstado);
                    command.Parameters.AddWithValue("@dtCadastro", cidade.DtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", cidade.DtAlteracao);
                    command.Parameters.AddWithValue("@status", cidade.Status);

                    await command.ExecuteNonQueryAsync();
                    return cidade;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cidades> Editar(Cidades cidade)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE cidades SET cidade = @cidade, codigoEstado = @codigoEstado, dtAlteracao = @dtAlteracao, status = @status";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@cidade", cidade.Cidade);
                    command.Parameters.AddWithValue("@codigoEstado", cidade.CodigoEstado);
                    command.Parameters.AddWithValue("@dtAlteracao", cidade.DtAlteracao);
                    command.Parameters.AddWithValue("@status", cidade.Status);
                    command.Parameters.AddWithValue("@codigo", cidade.Codigo);

                    await command.ExecuteNonQueryAsync();
                    return cidade;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Cidades cidade)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE paises SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM paises WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", cidade.Status);
                    command.Parameters.AddWithValue("@dtAlteracao", cidade.DtAlteracao);
                    command.Parameters.AddWithValue("@codigo", cidade.Codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Cidades>> Pesquisar(string str)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM cidades WHERE status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    IList<Cidades> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
    }
}
