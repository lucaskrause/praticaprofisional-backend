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
    public class ComprasDAO : DAO<Compras>
    {
        public ComprasDAO() : base()
        {
        }
        public async Task<List<ItensCompra>> GetItensCompraResultSet(NpgsqlCommand command)
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
        public async Task<List<ParcelasCompra>> GetParcelasCompraResultSet(NpgsqlCommand command)
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

        public async Task<List<ItensCompra>> BuscarItensCompra(NpgsqlConnection conexao, Compras compra)
        {
            string sql = @"SELECT produtoscompras.*, produtos.produto FROM produtoscompras INNER JOIN produtos ON produtos.codigo = produtoscompras.codigoProduto WHERE modelo = @modelo AND serie = @serie AND numeronf = @numeroNF AND codigofornecedor = @codigoFornecedor;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@modelo", compra.modelo);
            command.Parameters.AddWithValue("@serie", compra.serie);
            command.Parameters.AddWithValue("@numeroNF", compra.numeroNF);
            command.Parameters.AddWithValue("@codigoFornecedor", compra.codigoFornecedor);

            List<ItensCompra> itensCompra = await GetItensCompraResultSet(command);

            return itensCompra;
        }

        public async Task<List<ParcelasCompra>> BuscarParcelasCompra(NpgsqlConnection conexao, Compras compra)
        {
            string sql = @"SELECT contaspagar.numeroparcela, contaspagar.valorparcela, contaspagar.codigoformapagamento, contaspagar.dtvencimento, formaspagamento.descricao as descricaoForma FROM contaspagar INNER JOIN formaspagamento ON formaspagamento.codigo = contaspagar.codigoFormaPagamento WHERE modelo = @modelo AND serie = @serie AND numeronf = @numeroNF AND codigofornecedor = @codigoFornecedor;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@modelo", compra.modelo);
            command.Parameters.AddWithValue("@serie", compra.serie);
            command.Parameters.AddWithValue("@numeroNF", compra.numeroNF);
            command.Parameters.AddWithValue("@codigoFornecedor", compra.codigoFornecedor);

            List<ParcelasCompra> parcelas = await GetParcelasCompraResultSet(command);

            return parcelas;
        }

        public async Task AtualizarProdutos(NpgsqlConnection conexao, Compras compra, ItensCompra item)
        {
            string sql = @"UPDATE produtos SET dtultimacompra = @dtUltimaCompra, valorultimacompra = @valorUltimaCompra WHERE codigo = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@dtUltimaCompra", compra.dtEmissao);
            command.Parameters.AddWithValue("@valorUltimaCompra", item.valorUnitario);
            command.Parameters.AddWithValue("@codigo", item.codigoProduto);

            await command.ExecuteScalarAsync();
        }

        public async Task<ItensCompra> InserirItensCompra(NpgsqlConnection conexao, ItensCompra item)
        {
            string sql = @"INSERT INTO produtoscompras(modelo, serie, numeronf, codigofornecedor, codigoproduto, quantidade, valorunitario, desconto, total) VALUES (@modelo, @serie, @numeroNF, @codigoFornecedor, @codigoProduto, @quantidade, @valorUnitario, @desconto, @total);";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@modelo", item.modelo);
            command.Parameters.AddWithValue("@serie", item.serie);
            command.Parameters.AddWithValue("@numeroNF", item.numeroNF);
            command.Parameters.AddWithValue("@codigoFornecedor", item.codigoFornecedor);
            command.Parameters.AddWithValue("@codigoProduto", item.codigoProduto);
            command.Parameters.AddWithValue("@quantidade", item.quantidade);
            command.Parameters.AddWithValue("@valorUnitario", item.valorUnitario);
            command.Parameters.AddWithValue("@desconto", item.desconto);
            command.Parameters.AddWithValue("@total", item.total);

            await command.ExecuteScalarAsync();

            return item;
        }

        public async Task<ParcelasCompra> InserirParcelasCompra(NpgsqlConnection conexao, ParcelasCompra parcela, Compras compra)
        {
            string sql = @"INSERT INTO contaspagar(modelo, serie, numeronf, codigofornecedor, numeroparcela, valorparcela, codigoformapagamento, dtemissao, dtvencimento, dtpagamento, status) VALUES (@modelo, @serie, @numeroNF, @codigoFornecedor, @numeroParcela, @valorParcela, @codigoFormaPagamento, @dtEmissao, @dtVencimento, @dtPagamento, @status);";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@modelo", compra.modelo);
            command.Parameters.AddWithValue("@serie", compra.serie);
            command.Parameters.AddWithValue("@numeroNF", compra.numeroNF);
            command.Parameters.AddWithValue("@codigoFornecedor", compra.codigoFornecedor);
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

        public override async Task<IList<Compras>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT compras.*, fornecedores.nome as nomeFornecedor FROM compras INNER JOIN fornecedores ON codigoFornecedor = fornecedores.codigo WHERE compras.status = 'Ativo' ORDER BY compras.modelo, compras.serie, compras.numeronf;";

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

        public async Task<bool> Find(string modelo, string serie, string numeroNF, int codigoFornecedor)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM compras WHERE modelo = @modelo AND serie = @serie AND numeroNF = @numeroNF AND codigoFornecedor = @codigoFornecedor;"; // AND status = 'Ativo'

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@modelo", modelo);
                    command.Parameters.AddWithValue("@serie", serie);
                    command.Parameters.AddWithValue("@numeroNF", numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", codigoFornecedor);

                    List<Compras> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Compras> BuscarPorID(int codigo)
        {
            return null;
        }

        public async Task<Compras> BuscarCompra(Compras compra)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT compras.modelo, compras.serie, compras.numeronf, compras.codigofornecedor, compras.codigocondicaopagamento, compras.dtemissao, compras.dtentrega, compras.dtcadastro, compras.dtalteracao, compras.status, fornecedores.nome as nomeFornecedor, condicoespagamento.descricao as nomeCondicao FROM compras INNER JOIN fornecedores ON fornecedores.codigo = compras.codigofornecedor INNER JOIN condicoespagamento ON condicoespagamento.codigo = compras.codigocondicaopagamento WHERE modelo = @modelo AND serie = @serie AND numeroNF = @numeroNF AND codigoFornecedor = @codigoFornecedor;"; // AND status = 'Ativo'

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@modelo", compra.modelo);
                    command.Parameters.AddWithValue("@serie", compra.serie);
                    command.Parameters.AddWithValue("@numeroNF", compra.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", compra.codigoFornecedor);

                    List<Compras> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        list[0].itens = await BuscarItensCompra(conexao, compra);
                        list[0].parcelas = await BuscarParcelasCompra(conexao, compra);

                        transaction.Commit();
                        return list[0];
                    }
                    else
                    {
                        transaction.Rollback();
                        throw new Exception("Compra não encontrada");
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
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"INSERT INTO compras(modelo, serie, numeronf, codigofornecedor, codigocondicaopagamento, dtemissao, dtentrega, dtcadastro, dtalteracao, status) VALUES (@modelo, @serie, @numeroNF, @codigoFornecedor, @codigoCondicaoPagamento, @dtEmissao, @dtEntrega, @dtCadastro, @dtAlteracao, @status);";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@modelo", compra.modelo);
                    command.Parameters.AddWithValue("@serie", compra.serie);
                    command.Parameters.AddWithValue("@numeroNF", compra.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", compra.codigoFornecedor);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", compra.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtEmissao", compra.dtEmissao);
                    command.Parameters.AddWithValue("@dtEntrega", compra.dtEntrega);
                    command.Parameters.AddWithValue("@dtCadastro", compra.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", compra.dtAlteracao);
                    command.Parameters.AddWithValue("@status", compra.status);

                    await command.ExecuteScalarAsync();

                    int qtdItens = compra.itens.Count;
                    if (qtdItens > 0)
                    {
                        for (int i = 0; i < qtdItens; i++)
                        {
                            ItensCompra itemCompra = compra.itens[i];
                            itemCompra.modelo = compra.modelo;
                            itemCompra.serie = compra.serie;
                            itemCompra.numeroNF = compra.numeroNF;
                            itemCompra.codigoFornecedor = compra.codigoFornecedor;
                            compra.itens[i] = await InserirItensCompra(conexao, itemCompra);

                            await AtualizarProdutos(conexao, compra, itemCompra);
                        }
                    }

                    int qtdParcelas = compra.parcelas.Count;
                    if (qtdParcelas > 0)
                    {
                        for (int i = 0; i < qtdParcelas; i++)
                        {
                            ParcelasCompra parcelaCompra = compra.parcelas[i];
                            parcelaCompra.dtEmissao = compra.dtEmissao;
                            parcelaCompra.pendente();
                            compra.parcelas[i] = await InserirParcelasCompra(conexao, parcelaCompra, compra);
                        }
                    }

                    transaction.Commit();
                    return compra;
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

        public override async Task<Compras> Editar(Compras compra)
        {
            return null;
        }

        public override async Task<bool> Excluir(Compras compra)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE compras SET dtalteracao = @dtAlteracao, status = @status WHERE modelo = @modelo AND serie = @serie AND numeroNF = @numeroNF AND codigoFornecedor = @codigoFornecedor;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@dtAlteracao", compra.dtAlteracao);
                    command.Parameters.AddWithValue("@status", compra.status);
                    command.Parameters.AddWithValue("@modelo", compra.modelo);
                    command.Parameters.AddWithValue("@serie", compra.serie);
                    command.Parameters.AddWithValue("@numeroNF", compra.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", compra.codigoFornecedor);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    throw new Exception("Não foi possível cancelar a compra");
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
