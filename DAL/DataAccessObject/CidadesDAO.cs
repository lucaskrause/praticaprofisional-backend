using Npgsql;
using DAL.Models;
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
                    string sql = @"SELECT cidades.codigo, cidades.cidade, cidades.ddd, cidades.codigoestado, cidades.dtcadastro, cidades.dtalteracao, cidades.status, estados.uf as nomeUF FROM cidades INNER JOIN estados ON cidades.codigoestado = estados.codigo WHERE cidades.status = 'Ativo' ORDER BY cidades.codigo;";

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
                    string sql = @"SELECT cidades.codigo, cidades.cidade, cidades.ddd, cidades.codigoestado, cidades.dtcadastro, cidades.dtalteracao, cidades.status, estados.estado as nomeEstado FROM cidades INNER JOIN estados ON cidades.codigoestado = estados.codigo WHERE cidades.codigo = @codigo AND cidades.status = 'Ativo';";

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
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "cidades", "cidade", cidade.cidade);
                    if (exists)
                    {
                        string sql = @"INSERT INTO cidades (cidade, ddd, codigoEstado, dtCadastro, dtAlteracao, status) VALUES(@cidade, @ddd, @codigoEstado, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@cidade", cidade.cidade);
                        command.Parameters.AddWithValue("@ddd", cidade.ddd);
                        command.Parameters.AddWithValue("@codigoEstado", cidade.codigoEstado);
                        command.Parameters.AddWithValue("@dtCadastro", cidade.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", cidade.dtAlteracao);
                        command.Parameters.AddWithValue("@status", cidade.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        cidade.codigo = (int)idInserido;
                        return cidade;
                    }
                    else
                    {
                        throw new Exception("Cidade já cadastrada");
                    }
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
                    string sql = @"UPDATE cidades SET cidade = @cidade, ddd = @ddd, codigoEstado = @codigoEstado, dtAlteracao = @dtAlteracao, status = @status WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@cidade", cidade.cidade);
                    command.Parameters.AddWithValue("@ddd", cidade.ddd);
                    command.Parameters.AddWithValue("@codigoEstado", cidade.codigoEstado);
                    command.Parameters.AddWithValue("@dtAlteracao", cidade.dtAlteracao);
                    command.Parameters.AddWithValue("@status", cidade.status);
                    command.Parameters.AddWithValue("@codigo", cidade.codigo);

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
                    string sql = @"DELETE FROM paises WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", cidade.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE paises SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", cidade.status);
                    command.Parameters.AddWithValue("@dtAlteracao", cidade.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", cidade.codigo);

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
