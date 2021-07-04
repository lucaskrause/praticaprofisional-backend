using Npgsql;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class FormasPagamentoDAO : DAO<FormasPagamento>
    {
        public FormasPagamentoDAO() : base()
        {
        }

        public override async Task<IList<FormasPagamento>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM formasPagamento WHERE status = 'Ativo' ORDER BY codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<FormasPagamento> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
     
        public override async Task<FormasPagamento> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM formasPagamento WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<FormasPagamento> list = await GetResultSet(command);

                    if (list.Count > 0) {
                        return list[0];
                    } else
                    {
                        throw new Exception("Forma de Pagamento não encontrada");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<FormasPagamento> Inserir(FormasPagamento formasPagamento)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "formasPagamento", "descricao", formasPagamento.descricao);
                    if (exists)
                    {
                        string sql = @"INSERT INTO formasPagamento(descricao, dtCadastro, dtAlteracao, status) VALUES (@descricao, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@descricao", formasPagamento.descricao);
                        command.Parameters.AddWithValue("@dtCadastro", formasPagamento.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", formasPagamento.dtAlteracao);
                        command.Parameters.AddWithValue("@status", formasPagamento.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        formasPagamento.codigo = (int)idInserido;
                        return formasPagamento;
                    }
                    else
                    {
                        throw new Exception("Forma de Pagamento já cadastrada");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<FormasPagamento> Editar(FormasPagamento formasPagamento)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "formasPagamento", "descricao", formasPagamento.descricao, formasPagamento.codigo);
                    if (exists)
                    {
                        string sql = @"UPDATE formasPagamento SET descricao = @descricao, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@descricao", formasPagamento.descricao);
                        command.Parameters.AddWithValue("@dtAlteracao", formasPagamento.dtAlteracao);
                        command.Parameters.AddWithValue("@codigo", formasPagamento.codigo);

                        await command.ExecuteNonQueryAsync();
                        return formasPagamento;
                    }
                    else
                    {
                        throw new Exception("Forma de Pagamento já cadastrada");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(FormasPagamento formasPagamento)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM formasPagamento WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", formasPagamento.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE formasPagamento SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", formasPagamento.status);
                    command.Parameters.AddWithValue("@dtAlteracao", formasPagamento.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", formasPagamento.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<FormasPagamento>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
