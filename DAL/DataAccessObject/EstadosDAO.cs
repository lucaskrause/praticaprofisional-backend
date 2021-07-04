using Npgsql;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class EstadosDAO : DAO<Estados>
    {
        public EstadosDAO() : base()
        {
        }

        public override async Task<IList<Estados>> ListarTodos()
        {
            using(var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT estados.codigo, estados.estado, estados.uf, estados.codigopais, estados.dtCadastro, estados.dtAlteracao, paises.pais as nomePais FROM estados INNER JOIN paises ON (estados.codigopais = paises.codigo) WHERE estados.status = 'Ativo' ORDER BY estados.codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Estados> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Estados> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT estados.codigo, estados.estado, estados.uf, estados.codigopais, estados.dtCadastro, estados.dtAlteracao, paises.pais as nomePais FROM estados INNER JOIN paises ON (estados.codigopais = paises.codigo) WHERE estados.codigo = @codigo AND estados.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Estados> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Estados> Inserir(Estados estado)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "estados", "estado", estado.estado);
                    if (exists)
                    {
                        string sql = @"INSERT INTO estados (estado, uf, codigoPais, dtCadastro, dtAlteracao, status) VALUES (@estado, @uf, @codigoPais, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@estado", estado.estado);
                        command.Parameters.AddWithValue("@uf", estado.uf);
                        command.Parameters.AddWithValue("@codigoPais", estado.codigoPais);
                        command.Parameters.AddWithValue("@dtCadastro", estado.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", estado.dtAlteracao);
                        command.Parameters.AddWithValue("@status", estado.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        estado.codigo = (int) idInserido;
                        return estado;
                    }
                    else
                    {
                        throw new Exception("Estado já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Estados> Editar(Estados estado)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "estados", "estado", estado.estado, estado.codigo);
                    if (exists)
                    {
                        string sql = @"UPDATE estados SET estado = @estado, uf = @uf, codigoPais = @codigoPais, dtAlteracao = @dtAlteracao WHERE codigo = @codigo;";

                        conexao.Open();

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@estado", estado.estado);
                        command.Parameters.AddWithValue("@uf", estado.uf);
                        command.Parameters.AddWithValue("@codigoPais", estado.codigoPais);
                        command.Parameters.AddWithValue("@dtAlteracao", estado.dtAlteracao);
                        command.Parameters.AddWithValue("@codigo", estado.codigo);

                        await command.ExecuteNonQueryAsync();
                        return estado;
                    }
                    else
                    {
                        throw new Exception("Estado já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Estados estado)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM estados WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", estado.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE estados SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", estado.status);
                    command.Parameters.AddWithValue("@dtAlteracao", estado.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", estado.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Estados>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
