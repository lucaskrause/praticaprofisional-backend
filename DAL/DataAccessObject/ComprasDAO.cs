using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ComprasDAO : DAO<Compras>
    {
        public ComprasDAO() : base()
        {
        }

        public override async Task<IList<Compras>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM compraes WHERE status = 'Ativo' ORDER BY codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Compras> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Compras> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM compraes WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Compras> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        throw new Exception("País não encontrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Compras> Inserir(Compras compra)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "compraes", "compra", compra.compra);
                    if (exists)
                    {
                        string sql = @"INSERT INTO compraes(compra, sigla, ddi, dtCadastro, dtAlteracao, status) VALUES (@compra, @sigla, @ddi, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@compra", compra.compra);
                        command.Parameters.AddWithValue("@sigla", compra.sigla);
                        command.Parameters.AddWithValue("@ddi", compra.ddi);
                        command.Parameters.AddWithValue("@dtCadastro", compra.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", compra.dtAlteracao);
                        command.Parameters.AddWithValue("@status", compra.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        compra.codigo = (int)idInserido;
                        return compra;
                    }
                    else
                    {
                        throw new Exception("País já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Compras> Editar(Compras compra)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "compraes", "compra", compra.compra, compra.codigo);
                    if (exists)
                    {
                        string sql = @"UPDATE compraes SET compra = @compra, sigla = @sigla, ddi = @ddi, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@compra", compra.compra);
                        command.Parameters.AddWithValue("@sigla", compra.sigla);
                        command.Parameters.AddWithValue("@ddi", compra.ddi);
                        command.Parameters.AddWithValue("@dtAlteracao", compra.dtAlteracao);
                        command.Parameters.AddWithValue("@codigo", compra.codigo);

                        await command.ExecuteNonQueryAsync();
                        return compra;
                    }
                    else
                    {
                        throw new Exception("País já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Compras compra)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM compraes WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", compra.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    throw new Exception("Não é possivel excluir o País, pois está vinculado a um Estado");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Compras>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
