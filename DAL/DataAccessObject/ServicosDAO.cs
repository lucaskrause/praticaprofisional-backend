using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ServicosDAO : DAO<Servicos>
    {
        public ServicosDAO() : base()
        {
        }

        public override async Task<IList<Servicos>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM servicos WHERE status = 'Ativo'";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Servicos> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Servicos> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM servicos WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Servicos> list = await GetResultSet(command);

                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Servicos> Inserir(Servicos servico)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();

                try
                {
                    string sql = @"INSERT INTO servicos(descricao, valor, dtCadastro, dtAlteracao, status) VALUES (@descricao, @valor, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@descricao", servico.descricao);
                    command.Parameters.AddWithValue("@valor", servico.valor);
                    command.Parameters.AddWithValue("@dtCadastro", servico.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", servico.dtAlteracao);
                    command.Parameters.AddWithValue("@status", servico.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    servico.codigo = (int)idInserido;
                    return servico;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    transaction.Commit();
                    conexao.Close();
                }
            }
        }



        public override async Task<Servicos> Editar(Servicos servico)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE servicos SET descricao = @descricao, valor = @valor, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@descricao", servico.descricao);
                    command.Parameters.AddWithValue("@valor", servico.valor);
                    command.Parameters.AddWithValue("@dtAlteracao", servico.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", servico.codigo);

                    await command.ExecuteNonQueryAsync();
                    return servico;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Servicos servico)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE servicos SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM servicos WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", servico.status);
                    command.Parameters.AddWithValue("@dtAlteracao", servico.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", servico.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Servicos>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
