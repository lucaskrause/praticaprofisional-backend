using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ComprasDAO : DAO<Compras>
    {
        public ComprasDAO() : base()
        {
        }

        public async Task<ItensCompra> inserirItensCompra(NpgsqlConnection conexao, ItensCompra item)
        {
            string sql = @"INSERT INTO itens_compra(modelo, serie, numeronf, codigofornecedor, codigoproduto, quantidade, valorunitario, desconto, total) VALUES (@modelo, @serie, @numeroNF, @codigoFornecedor, @codigoProduto, @quantidade, @valorUnitario, @desconto, @total);";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
            command.Parameters.AddWithValue("@modelo", item.modelo);
            command.Parameters.AddWithValue("@serie", item.serie);
            command.Parameters.AddWithValue("@numeroNF", item.numeroNF);
            command.Parameters.AddWithValue("@codigoFornecedor", item.codigoFornecedor);
            command.Parameters.AddWithValue("@codigoProduto", item.codigo);
            command.Parameters.AddWithValue("@quantidade", item.quantidade);
            command.Parameters.AddWithValue("@valorUnitario", item.valorUnitario);
            command.Parameters.AddWithValue("@desconto", item.desconto);
            command.Parameters.AddWithValue("@total", item.total);

            await command.ExecuteScalarAsync();

            return item;
        }

        public override async Task<IList<Compras>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM compras WHERE status = 'Ativo' ORDER BY modelo, serie, numeronf;";

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

        public async Task<bool> Find(Compras compra)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM compras WHERE modelo = @modelo AND serie = @serie AND numeroNF = @numeroNF AND codigoFornecedor = @codigoFornecedor;"; // AND status = 'Ativo'

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@modelo", compra.modelo);
                    command.Parameters.AddWithValue("@serie", compra.serie);
                    command.Parameters.AddWithValue("@numeroNF", compra.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", compra.codigoFornecedor);

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
                try
                {
                    string sql = @"SELECT compras.modelo, compras.serie, compras.numeronf, compras.codigofornecedor, compras.codigocondicaopagamento, compras.dtemissao, compras.dtentrega, compras.dtcadastro, compras.dtalteracao, compras.status, fornecedores.nome as nomeFornecedor, condicoespagamento.descricao as nomeCondicao FROM compras INNER JOIN fornecedores ON fornecedores.codigo = compras.codigofornecedor INNER JOIN condicoespagamento ON condicoespagamento.codigo = compras.codigocondicaopagamento WHERE modelo = @modelo AND serie = @serie AND numeroNF = @numeroNF AND codigoFornecedor = @codigoFornecedor;"; // AND status = 'Ativo'

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@modelo", compra.modelo);
                    command.Parameters.AddWithValue("@serie", compra.serie);
                    command.Parameters.AddWithValue("@numeroNF", compra.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", compra.codigoFornecedor);

                    List<Compras> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
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
                    if(qtdItens > 0)
                    {
                        for (int i = 0; i < qtdItens; i++)
                        {
                            ItensCompra itemCompra = compra.itens[i];
                            itemCompra.modelo = compra.modelo;
                            itemCompra.serie = compra.serie;
                            itemCompra.numeroNF = compra.numeroNF;
                            itemCompra.codigoFornecedor = compra.codigoFornecedor;
                            compra.itens[i] = await inserirItensCompra(conexao, itemCompra);
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
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    string sql = @"UPDATE compras SET compra = @compra, sigla = @sigla, ddi = @ddi, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@dtAlteracao", compra.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return compra;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Compras compra)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE compras SET dtalteracao = @dtAlteracao, status = @status WHERE modelo = @codigo AND serie = @codigo AND numeroNF = @codigo AND codigoFornecedor = @codigoFornecedor;";

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
