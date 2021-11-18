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
    public class CotasDAO : DAO<Cotas>
    {
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

        public async Task<List<ContasReceber>> GetContasReceber(NpgsqlConnection conexao, int codigoCota)
        {
            string sql = @"SELECT contasreceber.codigo, contasreceber.numeroparcela, contasreceber.valorparcela, contasreceber.codigoformapagamento, contasreceber.codigocliente, contasreceber.codigolocacao, contasreceber.dtemissao, contasreceber.dtvencimento, contasreceber.dtpagamento, contasreceber.status, formasPagamento.descricao as descricaoForma FROM contasreceber LEFT JOIN formasPagamento ON contasreceber.codigoFormaPagamento = formasPagamento.codigo LEFT JOIN clientes ON contasreceber.codigocliente = clientes.codigo LEFT JOIN cotas ON contasreceber.codigoCota = cotas.codigo WHERE contasreceber.codigoCota = @codigo ORDER BY contasreceber.numeroParcela;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@codigo", codigoCota);

            List<ContasReceber> listContas = await GetContasReceberResultSet(command);

            return listContas;
        }

        public async Task<ContasReceber> InserirContasReceber(NpgsqlConnection conexao, ContasReceber conta, Cotas cota)
        {
            string sql = @"INSERT INTO contasreceber(numeroparcela, valorparcela, codigoformapagamento, codigocliente, codigocota, dtemissao, dtvencimento, dtpagamento, status) VALUES (@numeroParcela, @valorParcela, @codigoFormaPagamento, @codigoCliente, @codigoCota, @dtEmissao, @dtVencimento, @dtPagamento, @status);";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@numeroParcela", conta.numeroParcela);
            command.Parameters.AddWithValue("@valorParcela", conta.valorParcela);
            command.Parameters.AddWithValue("@codigoFormaPagamento", conta.codigoFormaPagamento);
            command.Parameters.AddWithValue("@codigoCliente", cota.codigoCliente);
            command.Parameters.AddWithValue("@codigoCota", cota.codigo);
            command.Parameters.AddWithValue("@dtEmissao", conta.dtEmissao);
            command.Parameters.AddWithValue("@dtVencimento", conta.dtVencimento);
            command.Parameters.AddWithValue("@dtPagamento", conta.dtPagamento ?? (Object)DBNull.Value);
            command.Parameters.AddWithValue("@status", conta.status);

            await command.ExecuteScalarAsync();

            return conta;
        }

        public async Task<bool> CancelarContasReceber(NpgsqlConnection conexao, Cotas cota)
        {
            string sql = @"UPDATE contasreceber SET status = @status WHERE codigocota = @codigo AND status != 'Pago';";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@status", cota.status);
            command.Parameters.AddWithValue("@codigo", cota.codigo);

            var result = await command.ExecuteNonQueryAsync();
            return result == 1 ? true : false;
        }

        public override async Task<IList<Cotas>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT cotas.codigo, cotas.codigocliente, cotas.valor, cotas.dtinicio, cotas.dttermino, cotas.dtcadastro, cotas.dtalteracao, cotas.status, clientes.nome as nomeCliente FROM cotas INNER JOIN clientes ON (cotas.codigocliente = clientes.codigo) WHERE cotas.status = 'Ativo' ORDER BY cotas.codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Cotas> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cotas> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT cotas.codigo, cotas.codigocliente, cotas.valor, cotas.dtinicio, cotas.dttermino, cotas.dtcadastro, cotas.dtalteracao, cotas.status, clientes.nome as nomeCliente FROM cotas INNER JOIN clientes ON (cotas.codigocliente = clientes.codigo) WHERE cotas.codigo = @codigo AND cotas.status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Cotas> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        list[0].parcelas = await GetContasReceber(conexao, codigo);
                    }
                    else
                    {
                        throw new Exception("Cota não encontrada");
                    }

                    transaction.Commit();
                    return list[0];
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Ocorreu um erro ao buscar a cota");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cotas> Inserir(Cotas cota)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"INSERT INTO cotas(codigocliente, valor, dtinicio, dttermino, dtcadastro, dtalteracao, status) VALUES (@codigoCliente, @valor, @dtInicio, @dtTermino, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoCliente", cota.codigoCliente);
                    command.Parameters.AddWithValue("@valor", cota.valor);
                    command.Parameters.AddWithValue("@dtInicio", cota.dtInicio);
                    command.Parameters.AddWithValue("@dtTermino", cota.dtTermino);
                    command.Parameters.AddWithValue("@dtCadastro", cota.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", cota.dtAlteracao);
                    command.Parameters.AddWithValue("@status", cota.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    cota.codigo = (int)idInserido;

                    int qtdParcelas = cota.parcelas.Count;
                    if (qtdParcelas > 0)
                    {
                        for (int i = 0; i < qtdParcelas; i++)
                        {
                            ContasReceber parcelaCompra = cota.parcelas[i];
                            parcelaCompra.pendente();
                            cota.parcelas[i] = await InserirContasReceber(conexao, parcelaCompra, cota);
                        }
                    }

                    transaction.Commit();
                    return cota;
                } catch
                {
                    transaction.Rollback();
                    throw new Exception("Ocorreu um erro ao salvar a cota");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Cotas> Editar(Cotas cota)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE cotas SET codigocliente = @codigoCliente, valor = @valor, dtinicio = @dtInicio, dttermino = @dtTermino, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoCliente", cota.codigoCliente);
                    command.Parameters.AddWithValue("@valor", cota.valor);
                    command.Parameters.AddWithValue("@dtInicio", cota.dtInicio);
                    command.Parameters.AddWithValue("@dtTermino", cota.dtTermino);
                    command.Parameters.AddWithValue("@dtAlteracao", cota.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", cota.codigo);

                    await command.ExecuteNonQueryAsync();
                    return cota;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Cotas cota)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"UPDATE cotas SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", cota.status);
                    command.Parameters.AddWithValue("@dtAlteracao", cota.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", cota.codigo);

                    var result = await command.ExecuteNonQueryAsync();

                    await CancelarContasReceber(conexao, cota);

                    transaction.Commit();
                    return result == 1 ? true : false;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Não foi possível cancelar a cota");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Cotas>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
