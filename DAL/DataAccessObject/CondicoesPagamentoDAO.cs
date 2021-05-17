using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class CondicoesPagamentoDAO : DAO<CondicoesPagamento>
    {
        public override async Task<IList<CondicoesPagamento>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<CondicoesPagamento> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<CondicoesPagamento> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<CondicoesPagamento> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<CondicoesPagamento> Inserir(CondicoesPagamento condicaoPagamento)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@descricao", condicaoPagamento.descricao);
                    command.Parameters.AddWithValue("@multa", condicaoPagamento.multa);
                    command.Parameters.AddWithValue("@juros", condicaoPagamento.juros);
                    command.Parameters.AddWithValue("@desconto", condicaoPagamento.desconto);
                    command.Parameters.AddWithValue("@dtCadastro", condicaoPagamento.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", condicaoPagamento.dtAlteracao);
                    command.Parameters.AddWithValue("@status", condicaoPagamento.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    condicaoPagamento.codigo = (int)idInserido;
                    return condicaoPagamento;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<CondicoesPagamento> Editar(CondicoesPagamento condicaoPagamento)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@descricao", condicaoPagamento.descricao);
                    command.Parameters.AddWithValue("@multa", condicaoPagamento.multa);
                    command.Parameters.AddWithValue("@juros", condicaoPagamento.juros);
                    command.Parameters.AddWithValue("@desconto", condicaoPagamento.desconto);
                    command.Parameters.AddWithValue("@dtAlteracao", condicaoPagamento.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return condicaoPagamento;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(CondicoesPagamento condicaoPagamento)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE condicoesPagamento SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM condicoesPagamento WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", condicaoPagamento.status);
                    command.Parameters.AddWithValue("@dtAlteracao", condicaoPagamento.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", condicaoPagamento.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
        
        public override async Task<IList<CondicoesPagamento>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
