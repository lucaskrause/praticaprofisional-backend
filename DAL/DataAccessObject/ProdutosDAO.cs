using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ProdutosDAO : DAO<Produtos>
    {
        public ProdutosDAO() : base()
        {
        }

        public override async Task<IList<Produtos>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT produtos.codigo, produtos.produto, produtos.unidades, produtos.valorcusto, produtos.estoque, produtos.codigocategoria, produtos.dtultimacompra, produtos.valorultimacompra, produtos.dtcadastro, produtos.dtalteracao, produtos.status, categorias.descricao as nomeCategoria FROM produtos INNER JOIN categorias ON(produtos.codigoCategoria = categorias.codigo) WHERE produtos.status = 'Ativo' ORDER BY produtos.codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Produtos> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Produtos> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT produtos.codigo, produtos.produto, produtos.unidades, produtos.valorcusto, produtos.estoque, produtos.codigocategoria, produtos.dtultimacompra, produtos.valorultimacompra, produtos.dtcadastro, produtos.dtalteracao, produtos.status, categorias.descricao as nomeCategoria FROM produtos INNER JOIN categorias ON(produtos.codigoCategoria = categorias.codigo) WHERE produtos.codigo = @codigo AND produtos.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Produtos> list = await GetResultSet(command);

                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Produtos> Inserir(Produtos produto)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "produtos", "produto", produto.produto);
                    if (exists)
                    {
                        string sql = @"INSERT INTO produtos(produto, unidades, valorcusto, estoque, codigocategoria, dtultimacompra, valorultimacompra, dtcadastro, dtalteracao, status) VALUES (@produto, @unidades, @valorCusto, @estoque, @codigoCategoria, @dtUltimaCompra, @valorUltimaCompra, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        conexao.Open();

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@produto", produto.produto);
                        command.Parameters.AddWithValue("@unidades", produto.unidades);
                        command.Parameters.AddWithValue("@valorCusto", produto.valorCusto);
                        command.Parameters.AddWithValue("@estoque", produto.estoque);
                        command.Parameters.AddWithValue("@codigoCategoria", produto.codigoCategoria);
                        command.Parameters.AddWithValue("@dtUltimaCompra", produto.dtUltimaCompra ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@valorUltimaCompra", produto.valorUltimaCompra ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@dtCadastro", produto.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", produto.dtAlteracao);
                        command.Parameters.AddWithValue("@status", produto.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        produto.codigo = (int)idInserido;
                        return produto;
                    } else
                    {
                        throw new Exception("Produto já cadastrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Produtos> Editar(Produtos produto)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE produtos SET produto = @produto, unidades = @unidades, valorcusto = @valorCusto, estoque = @estoque, codigocategoria = @codigoCategoria, dtultimacompra = @dtUltimaCompra, valorultimacompra = @valorUltimaCompra, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@produto", produto.produto);
                    command.Parameters.AddWithValue("@unidades", produto.unidades);
                    command.Parameters.AddWithValue("@valorCusto", produto.valorCusto);
                    command.Parameters.AddWithValue("@estoque", produto.estoque);
                    command.Parameters.AddWithValue("@codigoCategoria", produto.codigoCategoria);
                    command.Parameters.AddWithValue("@dtUltimaCompra", produto.dtUltimaCompra);
                    command.Parameters.AddWithValue("@valorUltimaCompra", produto.valorUltimaCompra);
                    command.Parameters.AddWithValue("@dtAlteracao", produto.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", produto.codigo);

                    await command.ExecuteNonQueryAsync();
                    return produto;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Produtos produto)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM produtos WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", produto.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE produtos SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", produto.status);
                    command.Parameters.AddWithValue("@dtAlteracao", produto.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", produto.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Produtos>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
