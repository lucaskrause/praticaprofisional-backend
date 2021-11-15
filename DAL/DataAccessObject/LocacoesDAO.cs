using DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class LocacoesDAO : DAO<Locacoes>
    {
        public async Task<List<AreasLocacoes>> GetAreasLocacoesResultSet(NpgsqlCommand command)
        {
            List<AreasLocacoes> list = new List<AreasLocacoes>();

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
                AreasLocacoes p = JsonConvert.DeserializeObject<AreasLocacoes>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<List<AreasLocacao>> GetAreasLocacaoResultSet(NpgsqlCommand command)
        {
            List<AreasLocacao> list = new List<AreasLocacao>();

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
                AreasLocacao p = JsonConvert.DeserializeObject<AreasLocacao>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<List<ContasReceber>> GetContasReceberResultSet(NpgsqlCommand command)
        {
            List<ContasReceber> list = new List<ContasReceber>();

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
                ContasReceber p = JsonConvert.DeserializeObject<ContasReceber>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<List<AreasLocacao>> GetAreasLocacoes(NpgsqlConnection conexao, int codigoLocacao)
        {
            string sql = @"SELECT * FROM areaslocacoes WHERE codigolocacao = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@codigo", codigoLocacao);

            List<AreasLocacoes> listAreasLocacoes = await GetAreasLocacoesResultSet(command);

            if (listAreasLocacoes.Count > 0)
            {
                List<AreasLocacao> areasLocacao = new List<AreasLocacao>();
                for (int i = 0; i < listAreasLocacoes.Count; i++)
                {
                    sql = @"SELECT * FROM areasLocacao WHERE codigo = @codigo;";

                    command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", listAreasLocacoes[i].codigoArea);

                    List<AreasLocacao> listAreasLocacao = await GetAreasLocacaoResultSet(command);

                    if (listAreasLocacao.Count > 0)
                    {
                        areasLocacao.Add(listAreasLocacao[0]);
                    }
                }
                return areasLocacao;
            }
            else
            {
                return new List<AreasLocacao>();
            }
        }

        public async Task<bool> InsertAreasLocacoes(NpgsqlConnection conexao, List<AreasLocacao> areasLocacao, int codigoLocacao)
        {
            for (int i = 0; i < areasLocacao.Count; i++)
            {
                AreasLocacao area = areasLocacao[i];
                area.Ativar();
                area.PrepareSave();

                string sql = @"INSERT INTO areaslocacoes(codigolocacao, codigoarea) VALUES (@codigoLocacao, @codigoArea);";
                
                NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                command.Parameters.AddWithValue("@codigoLocacao", codigoLocacao);
                command.Parameters.AddWithValue("@codigoArea", area.codigo);

                await command.ExecuteNonQueryAsync();
            }
            return true;
        }

        public async Task<bool> DeleteAreasLocacoes(NpgsqlConnection conexao, int codigoLocacao)
        {
            string sql = @"DELETE FROM areaslocacoes WHERE codigolocacao = @codigo";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@codigo", codigoLocacao);

            var result = await command.ExecuteNonQueryAsync();
            return result == 1 ? true : false;
        }

        public async Task<List<ContasReceber>> GetContasReceber(NpgsqlConnection conexao, int codigoLocacao)
        {
            string sql = @"SELECT contasreceber.codigo, contasreceber.numeroparcela, contasreceber.valorparcela, contasreceber.codigoformapagamento, contasreceber.codigocliente, contasreceber.codigolocacao, contasreceber.dtemissao, contasreceber.dtvencimento, contasreceber.dtpagamento, contasreceber.status, formasPagamento.descricao as descricaoForma FROM contasreceber LEFT JOIN formasPagamento ON contasreceber.codigoFormaPagamento = formasPagamento.codigo LEFT JOIN clientes ON contasreceber.codigocliente = clientes.codigo LEFT JOIN locacoes ON contasreceber.codigoLocacao = locacoes.codigo WHERE contasreceber.codigoLocacao = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@codigo", codigoLocacao);

            List<ContasReceber> listContas = await GetContasReceberResultSet(command);

            return listContas;
        }

        public async Task<ContasReceber> InserirContasReceber(NpgsqlConnection conexao, ContasReceber conta, Locacoes locacao)
        {
            string sql = @"INSERT INTO contasreceber(numeroparcela, valorparcela, codigoformapagamento, codigocliente, codigolocacao, dtemissao, dtvencimento, dtpagamento, status) VALUES (@numeroParcela, @valorParcela, @codigoFormaPagamento, @codigoCliente, @codigoLocacao, @dtEmissao, @dtVencimento, @dtPagamento, @status);";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@numeroParcela", conta.numeroParcela);
            command.Parameters.AddWithValue("@valorParcela", conta.valorParcela);
            command.Parameters.AddWithValue("@codigoFormaPagamento", conta.codigoFormaPagamento);
            command.Parameters.AddWithValue("@codigoCliente", locacao.codigoCliente);
            command.Parameters.AddWithValue("@codigoLocacao", locacao.codigo);
            command.Parameters.AddWithValue("@dtEmissao", conta.dtEmissao);
            command.Parameters.AddWithValue("@dtVencimento", conta.dtVencimento);
            command.Parameters.AddWithValue("@dtPagamento", conta.dtPagamento ?? (Object)DBNull.Value);
            command.Parameters.AddWithValue("@status", conta.status);

            await command.ExecuteScalarAsync();

            return conta;
        }

        public async Task<bool> CancelarContasReceber(NpgsqlConnection conexao, Locacoes locacao)
        {
            string sql = @"UPDATE contasreceber SET status = @status WHERE codigolocacao = @codigo";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@status", locacao.status);
            command.Parameters.AddWithValue("@codigo", locacao.codigo);

            var result = await command.ExecuteNonQueryAsync();
            return result == 1 ? true : false;
        }

        public async Task<string> VerificaDisponibilidadeArea(DateTime dtLocacao, List<AreasLocacao> areas)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT * FROM locacoes WHERE dtlocacao = @dtLocacao";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@dtLocacao", dtLocacao);

                    List<Locacoes> list = await GetResultSet(command);
                    if (list.Count > 0)
                    {
                        var ids = list.Select(locacao => "codigoLocacao = " + locacao.codigo.ToString()).ToArray();
                        string conditionCodigo = String.Join(" OR ", ids);
                        string conditionArea;

                        for (int i = 0; i < areas.Count(); i++)
                        {
                            conditionArea = "codigoArea = " + areas[i].codigo.ToString();

                            sql = @"SELECT * FROM areasLocacoes WHERE " + conditionCodigo + " AND " + conditionArea + ";";

                            command = new NpgsqlCommand(sql, conexao);

                            List<AreasLocacoes> listAreasLocacoes = await GetAreasLocacoesResultSet(command);

                            if (listAreasLocacoes.Count() > 0)
                            {
                                return areas[i].descricao + " já alocada";
                            }
                        }
                    }
                    transaction.Commit();
                    return null;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Não foi verificar a disponibilidade");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Locacoes>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT locacoes.codigo, locacoes.codigocliente, locacoes.qtdepessoas, locacoes.dtlocacao, locacoes.valor, locacoes.codigocondicaopagamento, locacoes.dtcadastro, locacoes.dtalteracao, locacoes.status, clientes.nome as nomeCliente, condicoespagamento.descricao AS nomeCondicao FROM locacoes INNER JOIN clientes ON(locacoes.codigocliente = clientes.codigo) INNER JOIN condicoespagamento ON (locacoes.codigocondicaopagamento = condicoespagamento.codigo) WHERE locacoes.status = 'Ativo' ORDER BY locacoes.codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Locacoes> list = await GetResultSet(command);
                    if (list.Count > 0)
                    {
                        Locacoes locacao = new Locacoes();
                        for (int i = 0; i < list.Count; i++)
                        {
                            locacao = list[i];
                            locacao.areasLocacao = await GetAreasLocacoes(conexao, locacao.codigo);
                        }
                    }
                    return list;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Não foi possivel listar as locações");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Locacoes> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT locacoes.codigo, locacoes.codigocliente, locacoes.qtdepessoas, locacoes.dtlocacao, locacoes.valor, locacoes.codigocondicaopagamento, locacoes.dtcadastro, locacoes.dtalteracao, locacoes.status, clientes.nome as nomeCliente, condicoespagamento.descricao AS nomeCondicao FROM locacoes INNER JOIN clientes ON(locacoes.codigocliente = clientes.codigo) INNER JOIN condicoespagamento ON (locacoes.codigocondicaopagamento = condicoespagamento.codigo) WHERE locacoes.codigo = @codigo AND locacoes.status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Locacoes> listLocacao = await GetResultSet(command);

                    if (listLocacao.Count > 0)
                    {
                        listLocacao[0].areasLocacao = await GetAreasLocacoes(conexao, codigo);
                        listLocacao[0].parcelas = await GetContasReceber(conexao, codigo);
                    }
                    else
                    {
                        throw new Exception("Locação não encontrada");
                    }

                    transaction.Commit();
                    return listLocacao[0];
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

        public override async Task<Locacoes> Inserir(Locacoes locacao)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"INSERT INTO locacoes(codigocliente, qtdepessoas, dtlocacao, valor, codigocondicaopagamento, dtcadastro, dtalteracao, status) VALUES (@codigoCliente, @qtdePessoas, @dtLocacao, @valor, @codigoCondicaoPagamento, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigoCliente", locacao.codigoCliente);
                    command.Parameters.AddWithValue("@qtdePessoas", locacao.qtdePessoas);
                    command.Parameters.AddWithValue("@dtLocacao", locacao.dtLocacao);
                    command.Parameters.AddWithValue("@valor", locacao.valor);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", locacao.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtCadastro", locacao.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", locacao.dtAlteracao);
                    command.Parameters.AddWithValue("@status", locacao.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    locacao.codigo = (int)idInserido;

                    int qtdAreasLocacao = locacao.areasLocacao.Count;
                    if (qtdAreasLocacao > 0)
                    {
                        await InsertAreasLocacoes(conexao, locacao.areasLocacao, locacao.codigo);
                    }

                    int qtdParcelas = locacao.parcelas.Count;
                    if (qtdParcelas > 0)
                    {
                        for (int i = 0; i < qtdParcelas; i++)
                        {
                            ContasReceber parcelaCompra = locacao.parcelas[i];
                            parcelaCompra.dtEmissao = new DateTime();
                            parcelaCompra.pendente();
                            locacao.parcelas[i] = await InserirContasReceber(conexao, parcelaCompra, locacao);
                        }
                    }

                    transaction.Commit();
                    return locacao;
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

        public override async Task<Locacoes> Editar(Locacoes locacao)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"UPDATE locacoes SET codigocliente = @codigoCliente, qtdepessoas = @qtdePessoas, dtlocacao = @dtLocacao , valor = @valor, codigocondicaopagamento = @codigoCondicaoPagamento, dtalteracao = @dtAlteracao WHERE codigo = @codigo AND status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigoCliente", locacao.codigoCliente);
                    command.Parameters.AddWithValue("@qtdePessoas", locacao.qtdePessoas);
                    command.Parameters.AddWithValue("@dtLocacao", locacao.dtLocacao);
                    command.Parameters.AddWithValue("@valor", locacao.valor);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", locacao.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtAlteracao", locacao.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", locacao.codigo);

                    await command.ExecuteNonQueryAsync();

                    await DeleteAreasLocacoes(conexao, locacao.codigo);

                    int qtdAreasLocacao = locacao.areasLocacao.Count;
                    if (qtdAreasLocacao > 0)
                    {
                        await InsertAreasLocacoes(conexao, locacao.areasLocacao, locacao.codigo);
                    }

                    transaction.Commit();
                    return locacao;
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

        public override async Task<bool> Excluir(Locacoes locacao)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"UPDATE locacoes SET status = @status, dtalteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@status", locacao.status);
                    command.Parameters.AddWithValue("@dtAlteracao", locacao.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", locacao.codigo);

                    var result = await command.ExecuteNonQueryAsync();

                    await CancelarContasReceber(conexao, locacao);

                    transaction.Commit();
                    return result == 1 ? true : false;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Não foi possivel cancelar a locação");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Locacoes>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
