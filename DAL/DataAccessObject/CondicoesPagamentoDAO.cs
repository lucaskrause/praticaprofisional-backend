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

        public async Task<List<CondicoesParcelas>> GetParcelas(NpgsqlConnection conexao, int codigoCondicao)
        {
            string sql = @"SELECT condicoesParcela.codigo, condicoesParcela.codigocondicaopagamento, condicoesParcela.numeroparcela, condicoesParcela.numerodias, condicoesParcela.porcentagem, condicoesParcela.codigoformapagamento, condicoesParcela.dtcadastro, condicoesParcela.dtalteracao, condicoesParcela.status, formasPagamento.descricao as descricaoForma FROM condicoesparcela INNER JOIN formasPagamento ON (condicoesParcela.codigoFormaPagamento = formasPagamento.codigo) WHERE condicoesParcela.codigoCondicaoPagamento = @codigo AND condicoesParcela.status = 'Ativo';";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoCondicao);

            List<CondicoesParcelas> listParcelas = await GetParcelasResultSet(command);
            return listParcelas;
        }

        public async Task<CondicoesParcelas> InsertParcela(NpgsqlConnection conexao, CondicoesParcelas parcela)
        {
            string sql = @"INSERT INTO condicoesparcela(codigocondicaopagamento, numeroparcela, numerodias, porcentagem, codigoformapagamento, dtcadastro, dtalteracao, status) VALUES (@codigoCondicaoPagamento, @numeroParcela, @numeroDias, @porcentagem, @codigoFormaPagamento, @dtCadastro, @dtAlteracao, @status) returning codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigoCondicaoPagamento", parcela.codigoCondicaoPagamento);
            command.Parameters.AddWithValue("@numeroParcela", parcela.numeroParcela);
            command.Parameters.AddWithValue("@numeroDias", parcela.numeroDias);
            command.Parameters.AddWithValue("@porcentagem", parcela.porcentagem);
            command.Parameters.AddWithValue("@codigoFormaPagamento", parcela.codigoFormaPagamento);
            command.Parameters.AddWithValue("@dtCadastro", parcela.dtCadastro);
            command.Parameters.AddWithValue("@dtAlteracao", parcela.dtAlteracao);
            command.Parameters.AddWithValue("@status", parcela.status);

            Object idInserido = await command.ExecuteScalarAsync();
            parcela.codigo = (int)idInserido;
            return parcela;
        }

        public async Task<CondicoesParcelas> UpdateParcela(NpgsqlConnection conexao, CondicoesParcelas parcela)
        {
            string sql = @"UPDATE condicoesparcela SET codigocondicaopagamento = @codigoCondicaoPagamento, numeroparcela = @numeroParcela, numerodias = @numeroDias, porcentagem = @porcentagem, codigoformapagamento = @codigoFormaPagamento, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigoCondicaoPagamento", parcela.codigoCondicaoPagamento);
            command.Parameters.AddWithValue("@numeroParcela", parcela.numeroParcela);
            command.Parameters.AddWithValue("@numeroDias", parcela.numeroDias);
            command.Parameters.AddWithValue("@porcentagem", parcela.porcentagem);
            command.Parameters.AddWithValue("@codigoFormaPagamento", parcela.codigoFormaPagamento);
            command.Parameters.AddWithValue("@dtAlteracao", parcela.dtAlteracao);
            command.Parameters.AddWithValue("@codigo", parcela.codigo);

            await command.ExecuteNonQueryAsync();
            return parcela;
        }

        public async Task<bool> DeleteParcela(NpgsqlConnection conexao, int codigoParcela)
        {
            string sql = @"DELETE FROM condicoesparcela WHERE codigo = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoParcela);

            var result = await command.ExecuteNonQueryAsync();
            return result == 1 ? true : false;
        }

        public override async Task<IList<CondicoesPagamento>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM condicoesPagamento WHERE status = 'Ativo' ORDER BY codigo;";

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
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT * FROM condicoesPagamento WHERE codigo = @codigo AND status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<CondicoesPagamento> list = await GetResultSet(command);

                    if(list.Count > 0)
                    {
                        list[0].parcelas = await GetParcelas(conexao, codigo);
                    }

                    transaction.Commit();
                    return list[0];
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

        public override async Task<CondicoesPagamento> Inserir(CondicoesPagamento condicaoPagamento)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    bool exists = await CheckExist(conexao, "condicoespagamento", "descricao", condicaoPagamento.descricao);
                    if (exists)
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
                                condicaoPagamento.parcelas[i] = await InsertParcela(conexao, parcela);
                            }
                        }

                        transaction.Commit();
                        return condicaoPagamento;
                    }
                    else
                    {
                        throw new Exception("Condição de Pagamento já cadastrado");
                    }
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
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"UPDATE condicoespagamento SET descricao = @descricao, totalparcelas = @totalParcelas, multa = @multa, juros = @juros, desconto = @desconto, dtalteracao = @dtAlteracao WHERE codigo = @codigo AND status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@descricao", condicaoPagamento.descricao);
                    command.Parameters.AddWithValue("@totalParcelas", condicaoPagamento.totalParcelas);
                    command.Parameters.AddWithValue("@multa", condicaoPagamento.multa);
                    command.Parameters.AddWithValue("@juros", condicaoPagamento.juros);
                    command.Parameters.AddWithValue("@desconto", condicaoPagamento.desconto);
                    command.Parameters.AddWithValue("@dtAlteracao", condicaoPagamento.dtAlteracao);

                    await command.ExecuteNonQueryAsync();

                    int qtdParcelas = condicaoPagamento.parcelas.Count;
                    if (qtdParcelas > 0)
                    {
                        for (int i = 0; i < qtdParcelas; i++)
                        {
                            CondicoesParcelas parcela = condicaoPagamento.parcelas[i];
                            parcela.PrepareSave();
                            if (parcela.codigo == 0) {
                                parcela.codigoCondicaoPagamento = condicaoPagamento.codigo;
                                parcela.Ativar();
                                condicaoPagamento.parcelas[i] = await InsertParcela(conexao, parcela);
                            }
                            else if (parcela.codigo > 0 && parcela.status == "Ativo")
                            {
                                condicaoPagamento.parcelas[i] = await UpdateParcela(conexao, parcela);
                            }
                            else
                            {
                                await DeleteParcela(conexao, parcela.codigo);
                                condicaoPagamento.parcelas.RemoveAt(i);
                            }
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

        public override async Task<bool> Excluir(CondicoesPagamento condicaoPagamento)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"DELETE FROM condicoesParcela WHERE codigoCondicaoPagamento = @codigo;
                                   DELETE FROM condicoesPagamento WHERE codigo = @codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", condicaoPagamento.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Não foi possível excluir a condição de pagamento");
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
