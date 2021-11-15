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
    public class OrdensServicoDAO : DAO<OrdensServico>
    {
        public async Task<List<ServicosOS>> GetServicosOrdemServicoResultSet(NpgsqlCommand command)
        {
            List<ServicosOS> list = new List<ServicosOS>();

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
                ServicosOS p = JsonConvert.DeserializeObject<ServicosOS>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<List<ItensCompra>> GetItensOrdemServicoResultSet(NpgsqlCommand command)
        {
            List<ItensCompra> list = new List<ItensCompra>();

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
                ItensCompra p = JsonConvert.DeserializeObject<ItensCompra>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<List<ParcelasCompra>> GetParcelasOrdemServicoResultSet(NpgsqlCommand command)
        {
            List<ParcelasCompra> list = new List<ParcelasCompra>();

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
                ParcelasCompra p = JsonConvert.DeserializeObject<ParcelasCompra>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<List<ServicosOS>> BuscarServicosOrdemServico(NpgsqlConnection conexao, int codigoOS)
        {
            string sql = @"SELECT servicosos.*, servicos.descricao FROM servicosos JOIN servicos ON servicosos.codigoServico = servicos.codigo WHERE codigoos = @codigoOS;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@codigoOS", codigoOS);

            List<ServicosOS> itensCompra = await GetServicosOrdemServicoResultSet(command);

            return itensCompra;
        }

        public async Task<List<ItensCompra>> BuscarItensOrdemServico(NpgsqlConnection conexao, int codigoOS)
        {
            string sql = @"SELECT itensos.*, produtos.produto FROM itensos JOIN produtos ON produtos.codigo = itensos.codigoProduto WHERE codigoos = @codigoOS;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@codigoOS", codigoOS);

            List<ItensCompra> itensCompra = await GetItensOrdemServicoResultSet(command);

            return itensCompra;
        }

        public async Task<List<ParcelasCompra>> BuscarParcelasOrdemServico(NpgsqlConnection conexao, int codigoOS)
        {
            string modelo = "99";
            string serie = "99";

            string sql = @"SELECT contaspagar.numeroparcela, contaspagar.valorparcela, contaspagar.codigoformapagamento, contaspagar.dtvencimento, contaspagar.status, formaspagamento.descricao as descricaoForma FROM contaspagar INNER JOIN formaspagamento ON formaspagamento.codigo = contaspagar.codigoFormaPagamento WHERE modelo = @modelo AND serie = @serie AND numeronf = @numeroNF;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("modelo", modelo);
            command.Parameters.AddWithValue("serie", serie);
            command.Parameters.AddWithValue("@numeroNF", codigoOS.ToString());

            List<ParcelasCompra> parcelas = await GetParcelasOrdemServicoResultSet(command);

            return parcelas;
        }

        public async Task<ItensCompra> InserirItensOrdemServico(NpgsqlConnection conexao, ItensCompra item, int codigoOS)
        {
            string sql = @"INSERT INTO itensos(codigoos, codigoproduto, quantidade, valorunitario, desconto, total) VALUES (@codigoOS, @codigoProduto, @quantidade, @valorUnitario, @desconto, @total);";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@codigoOS", codigoOS);
            command.Parameters.AddWithValue("@codigoProduto", item.codigoProduto);
            command.Parameters.AddWithValue("@quantidade", item.quantidade);
            command.Parameters.AddWithValue("@valorUnitario", item.valorUnitario);
            command.Parameters.AddWithValue("@desconto", item.desconto);
            command.Parameters.AddWithValue("@total", item.total);

            await command.ExecuteScalarAsync();

            return item;
        }

        public async Task<ServicosOS> InserirServicosOrdemServico(NpgsqlConnection conexao, ServicosOS servico, int codigoOS)
        {
            string sql = @"INSERT INTO servicosos(codigoos, codigoservico, quantidade, valorunitario, total) VALUES (@codigoOS, @codigoServico, @quantidade, @valorUnitario, @total);";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@codigoOS", codigoOS);
            command.Parameters.AddWithValue("@codigoServico", servico.codigoServico);
            command.Parameters.AddWithValue("@quantidade", servico.quantidade);
            command.Parameters.AddWithValue("@valorUnitario", servico.valorUnitario);
            command.Parameters.AddWithValue("@total", servico.total);

            await command.ExecuteScalarAsync();

            return servico;
        }

        public async Task<ParcelasCompra> InserirParcelasOrdemServico(NpgsqlConnection conexao, ParcelasCompra parcela, OrdensServico ordemServico)
        {
            string modelo = "99";
            string serie = "99";

            string sql = @"INSERT INTO contaspagar(modelo, serie, numeronf, codigofornecedor, numeroparcela, valorparcela, codigoformapagamento, dtemissao, dtvencimento, dtpagamento, status) VALUES (@modelo, @serie, @numeroNF, @codigoFornecedor, @numeroParcela, @valorParcela, @codigoFormaPagamento, @dtEmissao, @dtVencimento, @dtPagamento, @status);";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@modelo", modelo);
            command.Parameters.AddWithValue("@serie", serie);
            command.Parameters.AddWithValue("@numeroNF", ordemServico.codigo);
            command.Parameters.AddWithValue("@codigoFornecedor", ordemServico.codigoFornecedor);
            command.Parameters.AddWithValue("@numeroParcela", parcela.numeroParcela);
            command.Parameters.AddWithValue("@valorParcela", parcela.valorParcela);
            command.Parameters.AddWithValue("@codigoFormaPagamento", parcela.codigoFormaPagamento);
            command.Parameters.AddWithValue("@dtEmissao", parcela.dtEmissao);
            command.Parameters.AddWithValue("@dtVencimento", parcela.dtVencimento);
            command.Parameters.AddWithValue("@dtPagamento", parcela.dtPagamento ?? (Object)DBNull.Value);
            command.Parameters.AddWithValue("@status", parcela.status);

            await command.ExecuteScalarAsync();

            return parcela;
        }

        public async Task<bool> CancelarContasPagar(NpgsqlConnection conexao, OrdensServico ordemServico)
        {
            string modelo = "99";
            string serie = "99";
            string statusConta = "Cancelado";

            string sql = @"UPDATE contaspagar SET status = @status WHERE modelo = @modelo AND serie = @serie AND numeronf = @numeroNF AND codigofornecedor = @codigoFornecedor;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@status", statusConta);
            command.Parameters.AddWithValue("@modelo", modelo);
            command.Parameters.AddWithValue("@serie", serie);
            command.Parameters.AddWithValue("@numeronf", ordemServico.codigo.ToString());
            command.Parameters.AddWithValue("@codigoFornecedor", ordemServico.codigoFornecedor);

            await command.ExecuteNonQueryAsync();

            return true;
        }

        public override async Task<IList<OrdensServico>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT ordensservico.codigo, ordensservico.codigofornecedor, ordensservico.dtinicial, ordensservico.dtfinal, ordensservico.valorservicos, ordensservico.valorprodutos, ordensservico.valortotal, ordensservico.codigocondicaopagamento, ordensservico.dtcadastro, ordensservico.dtalteracao, ordensservico.status, fornecedores.nome as nomeFornecedor, condicoespagamento.descricao FROM ordensservico JOIN fornecedores ON fornecedores.codigo = ordensservico.codigofornecedor JOIN condicoespagamento ON condicoespagamento.codigo = ordensservico.codigocondicaopagamento WHERE ordensservico.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<OrdensServico> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<OrdensServico> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT ordensservico.codigo, ordensservico.codigofornecedor, ordensservico.dtinicial, ordensservico.dtfinal, ordensservico.valorservicos, ordensservico.valorprodutos, ordensservico.valortotal, ordensservico.codigocondicaopagamento, ordensservico.dtcadastro, ordensservico.dtalteracao, ordensservico.status, fornecedores.nome as nomeFornecedor, condicoespagamento.descricao as nomeCondicao FROM ordensservico JOIN fornecedores ON fornecedores.codigo = ordensservico.codigofornecedor JOIN condicoespagamento ON condicoespagamento.codigo = ordensservico.codigocondicaopagamento WHERE ordensservico.codigo = @codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<OrdensServico> list = await GetResultSet(command);
                    if (list.Count > 0)
                    {
                        list[0].servicos = await BuscarServicosOrdemServico(conexao, codigo);
                        list[0].itens = await BuscarItensOrdemServico(conexao, codigo);
                        list[0].parcelas = await BuscarParcelasOrdemServico(conexao, codigo);

                        transaction.Commit();
                        return list[0];
                    } else
                    {
                        throw new Exception("Não foi possivel encontrar a ordem de serviço");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<OrdensServico> Inserir(OrdensServico ordemServico)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"INSERT INTO ordensservico(codigofornecedor, dtinicial, dtfinal, valorservicos, valorprodutos, valortotal, codigocondicaopagamento, dtcadastro, dtalteracao, status) VALUES (@codigoFornecedor, @dtInicial, @dtFinal, @valorServicos, @valorProdutos, @valorTotal, @codigoCondicaoPagamento, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigoFornecedor", ordemServico.codigoFornecedor);
                    command.Parameters.AddWithValue("@dtInicial", ordemServico.dtInicial);
                    command.Parameters.AddWithValue("@dtFinal", ordemServico.dtFinal);
                    command.Parameters.AddWithValue("@valorServicos", ordemServico.valorServicos);
                    command.Parameters.AddWithValue("@valorProdutos", ordemServico.valorProdutos);
                    command.Parameters.AddWithValue("@valorTotal", ordemServico.valorTotal);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", ordemServico.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtCadastro", ordemServico.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", ordemServico.dtAlteracao);
                    command.Parameters.AddWithValue("@status", ordemServico.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    ordemServico.codigo = (int)idInserido;

                    int qtdServicos = ordemServico.servicos.Count;
                    if (qtdServicos > 0)
                    {
                        for (int i = 0; i < qtdServicos; i++)
                        {
                            ServicosOS servicoOrdemServico = ordemServico.servicos[i];
                            ordemServico.servicos[i] = await InserirServicosOrdemServico(conexao, servicoOrdemServico, ordemServico.codigo);
                        }
                    }

                    int qtdItens = ordemServico.itens.Count;
                    if (qtdItens > 0)
                    {
                        for (int i = 0; i < qtdItens; i++)
                        {
                            ItensCompra itemOrdemServico = ordemServico.itens[i];
                            ordemServico.itens[i] = await InserirItensOrdemServico(conexao, itemOrdemServico, ordemServico.codigo);
                        }
                    }

                    int qtdParcelas = ordemServico.parcelas.Count;
                    if (qtdParcelas > 0)
                    {
                        for (int i = 0; i < qtdParcelas; i++)
                        {
                            ParcelasCompra parcelaordemServico = ordemServico.parcelas[i];
                            parcelaordemServico.dtEmissao = (DateTime)ordemServico.dtCadastro;
                            parcelaordemServico.pendente();
                            ordemServico.parcelas[i] = await InserirParcelasOrdemServico(conexao, parcelaordemServico, ordemServico);
                        }
                    }

                    transaction.Commit();
                    return ordemServico;
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

        public override Task<OrdensServico> Editar(OrdensServico ordemServico)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Excluir(OrdensServico ordemServico)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"UPDATE ordensservico SET dtalteracao = @dtAlteracao, status = @status WHERE codigo = @codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@dtAlteracao", ordemServico.dtAlteracao);
                    command.Parameters.AddWithValue("@status", ordemServico.status);
                    command.Parameters.AddWithValue("@codigo", ordemServico.codigo);

                    var result = await command.ExecuteNonQueryAsync();

                    await CancelarContasPagar(conexao, ordemServico);

                    transaction.Commit();

                    return result == 1 ? true : false;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Não foi possível cancelar a ordem de serviço");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<OrdensServico>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
