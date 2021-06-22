using DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class CondicoesPagamentoDAO : DAO<CondicoesPagamento>
    {
        public async Task<List<CondicoesParcelas>> GetParcelasResultSet(NpgsqlCommand command)
        {
            List<CondicoesParcelas> list = new List<CondicoesParcelas>();

            command.ExecuteNonQuery();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                DataTable schemaTable = reader.GetSchemaTable();

                JTokenWriter writer = new JTokenWriter();
                writer.WriteStartObject();

                foreach (DataRow row in schemaTable.Rows)
                {
                    writer.WritePropertyName(row[0].ToString());
                    writer.WriteValue(reader[row[0].ToString()]);
                }
                writer.WriteEndObject();
                JObject o = (JObject)writer.Token;
                var stringJson = o.ToString();
                CondicoesParcelas p = JsonConvert.DeserializeObject<CondicoesParcelas>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public override async Task<IList<CondicoesPagamento>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM condicoesPagamento WHERE status = 'Ativo';";

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
                    string sql = @"SELECT * FROM condicoesPagamento WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<CondicoesPagamento> list = await GetResultSet(command);

                    if(list.Count > 0)
                    {
                        sql = @"SELECT condicoesParcela.codigo, condicoesParcela.codigocondicaopagamento, condicoesParcela.numeroparcela, condicoesParcela.numerodias, condicoesParcela.porcentagem, condicoesParcela.codigoformapagamento, condicoesParcela.dtcadastro, condicoesParcela.dtalteracao, condicoesParcela.status, formasPagamento.descricao as descricaoForma FROM condicoesparcela INNER JOIN formasPagamento ON (condicoesParcela.codigoFormaPagamento = formasPagamento.codigo) WHERE condicoesParcela.codigoCondicaoPagamento = 6 AND condicoesParcela.status = 'Ativo';";

                        command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@codigo", codigo);

                        List<CondicoesParcelas> listParcelas = await GetParcelasResultSet(command);

                        list[0].parcelas = listParcelas;
                    }

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
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();

                try
                {
                    string sql = @"INSERT INTO condicoespagamento(descricao, totalparcelas, multa, juros, desconto, dtcadastro, dtalteracao, status) VALUES (@descricao, @totalParcelas, @multa, @juros, @desconto, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@descricao", condicaoPagamento.descricao);
                    command.Parameters.AddWithValue("@totalParcelas", condicaoPagamento.totalParcelas);
                    command.Parameters.AddWithValue("@multa", condicaoPagamento.multa);
                    command.Parameters.AddWithValue("@juros", condicaoPagamento.juros);
                    command.Parameters.AddWithValue("@desconto", condicaoPagamento.desconto);
                    command.Parameters.AddWithValue("@dtCadastro", condicaoPagamento.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", condicaoPagamento.dtAlteracao);
                    command.Parameters.AddWithValue("@status", condicaoPagamento.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    condicaoPagamento.codigo = (int)idInserido;

                    int qtdParcelas = condicaoPagamento.parcelas.Count;
                    if (qtdParcelas > 0)
                    {
                        for (int i = 0; i < qtdParcelas; i++)
                        {
                            CondicoesParcelas parcela = condicaoPagamento.parcelas[i];
                            parcela.codigoCondicaoPagamento = condicaoPagamento.codigo;
                            parcela.Ativar();
                            parcela.PrepareSave();

                            sql = @"INSERT INTO condicoesparcela(codigocondicaopagamento, numeroparcela, numerodias, porcentagem, codigoformapagamento, dtcadastro, dtalteracao, status) VALUES (@codigoCondicaoPagamento, @numeroParcela, @numeroDias, @porcentagem, @codigoFormaPagamento, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                            command = new NpgsqlCommand(sql, conexao);

                            command.Parameters.AddWithValue("@codigoCondicaoPagamento", parcela.codigoCondicaoPagamento);
                            command.Parameters.AddWithValue("@numeroParcela", parcela.numeroParcela);
                            command.Parameters.AddWithValue("@numeroDias", parcela.numeroDias);
                            command.Parameters.AddWithValue("@porcentagem", parcela.porcentagem);
                            command.Parameters.AddWithValue("@codigoFormaPagamento", parcela.codigoFormaPagamento);
                            command.Parameters.AddWithValue("@dtCadastro", parcela.dtCadastro);
                            command.Parameters.AddWithValue("@dtAlteracao", parcela.dtAlteracao);
                            command.Parameters.AddWithValue("@status", parcela.status);

                            idInserido = await command.ExecuteScalarAsync();
                            parcela.codigo = (int)idInserido;
                            condicaoPagamento.parcelas[i] = parcela;
                        }
                    }

                    transaction.Commit();
                    return condicaoPagamento;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
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
