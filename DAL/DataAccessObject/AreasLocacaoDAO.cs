using Npgsql;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class AreasLocacaoDAO : DAO<AreasLocacao>
    {
        public AreasLocacaoDAO() : base()
        {
        }

        public override async Task<IList<AreasLocacao>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM areasLocacao WHERE status = 'Ativo' ORDER BY codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<AreasLocacao> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
     
        public override async Task<AreasLocacao> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM areasLocacao WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<AreasLocacao> list = await GetResultSet(command);

                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<AreasLocacao> Inserir(AreasLocacao area)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "areasLocacao", "descricao", area.descricao);
                    if (exists)
                    {
                        string sql = @"INSERT INTO areasLocacao(descricao, valor, dtCadastro, dtAlteracao, status) VALUES (@descricao, @valor, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        conexao.Open();

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@descricao", area.descricao);
                        command.Parameters.AddWithValue("@valor", area.valor);
                        command.Parameters.AddWithValue("@dtCadastro", area.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", area.dtAlteracao);
                        command.Parameters.AddWithValue("@status", area.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        area.codigo = (int)idInserido;
                        return area;
                    }
                    else
                    {
                        throw new Exception("Área já cadastrada");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<AreasLocacao> Editar(AreasLocacao area)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "areasLocacao", "descricao", area.descricao, area.codigo);
                    if (exists)
                    {
                        string sql = @"UPDATE areasLocacao SET descricao = @descricao, valor = @valor, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                        conexao.Open();

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@descricao", area.descricao);
                        command.Parameters.AddWithValue("@valor", area.valor);
                        command.Parameters.AddWithValue("@dtAlteracao", area.dtAlteracao);
                        command.Parameters.AddWithValue("@codigo", area.codigo);

                        await command.ExecuteNonQueryAsync();
                        return area;
                    }
                    else
                    {
                        throw new Exception("Área já cadastrada");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(AreasLocacao area)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM areasLocacao WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", area.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE areasLocacao SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", area.status);
                    command.Parameters.AddWithValue("@dtAlteracao", area.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", area.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<AreasLocacao>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
