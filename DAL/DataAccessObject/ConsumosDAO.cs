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
    public class ConsumosDAO : DAO<Consumos>
    {
        public ConsumosDAO() : base()
        {
        }

        public async Task<List<Produtos>> GetProdutosResultSet(NpgsqlCommand command)
        {
            List<Produtos> list = new List<Produtos>();

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
                Produtos p = JsonConvert.DeserializeObject<Produtos>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public override async Task<IList<Consumos>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT consumos.codigo, consumos.codigofuncionario, consumos.codigoproduto, consumos.quantidade, consumos.observacao, consumos.dtcadastro, consumos.dtalteracao, consumos.status, funcionarios.nome as nomeFuncionario, produtos.produto as nomeProduto FROM consumos INNER JOIN funcionarios ON funcionarios.codigo = consumos.codigoFuncionario INNER JOIN produtos ON produtos.codigo = consumos.codigoProduto ORDER BY codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Consumos> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Consumos> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT consumos.codigo, consumos.codigofuncionario, consumos.codigoproduto, consumos.quantidade, consumos.observacao, consumos.dtcadastro, consumos.dtalteracao, consumos.status, funcionarios.nome as nomeFuncionario, produtos.produto as nomeProduto FROM consumos INNER JOIN funcionarios ON funcionarios.codigo = consumos.codigoFuncionario INNER JOIN produtos ON produtos.codigo = consumos.codigoProduto WHERE consumos.codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Consumos> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        throw new Exception("Consumo não encontrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Consumos> Inserir(Consumos consumo)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT estoque FROM produtos WHERE codigo = @codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", consumo.codigoProduto);

                    List<Produtos> produtos = await GetProdutosResultSet(command);
                    int estoqueAtual = produtos[0].estoque - consumo.quantidade;

                    sql = @"UPDATE produtos SET estoque = @estoque, dtAlteracao = @dtAlteracao WHERE codigo = @codigo;";

                    command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@estoque", estoqueAtual);
                    command.Parameters.AddWithValue("@dtAlteracao", consumo.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", consumo.codigoProduto);

                    await command.ExecuteScalarAsync();

                    sql = @"INSERT INTO consumos(codigofuncionario, codigoproduto, quantidade, observacao, dtCadastro, dtAlteracao, status) VALUES (@codigoFuncionario, @codigoProduto, @quantidade, @observacao, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoFuncionario", consumo.codigoFuncionario);
                    command.Parameters.AddWithValue("@codigoProduto", consumo.codigoProduto);
                    command.Parameters.AddWithValue("@quantidade", consumo.quantidade);
                    command.Parameters.AddWithValue("@observacao", consumo.observacao ?? (Object)DBNull.Value);
                    command.Parameters.AddWithValue("@dtCadastro", consumo.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", consumo.dtAlteracao);
                    command.Parameters.AddWithValue("@status", consumo.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    consumo.codigo = (int)idInserido;

                    transaction.Commit();
                    return consumo;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Não foi possível inserir o consumo");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Consumos> Editar(Consumos consumo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE consumos SET codigofuncionario = @codigoFuncionario, observacao = @observacao, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoFuncionario", consumo.codigoFuncionario);
                    command.Parameters.AddWithValue("@observacao", consumo.observacao ?? (Object)DBNull.Value);
                    command.Parameters.AddWithValue("@dtAlteracao", consumo.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", consumo.codigo);

                    await command.ExecuteNonQueryAsync();
                    return consumo;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Consumos consumo)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT * FROM consumos WHERE codigo = @codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", consumo.codigo);

                    List<Consumos> list = await GetResultSet(command);
                    consumo = list[0];

                    sql = @"SELECT estoque FROM produtos WHERE codigo = @codigo;";

                    command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", consumo.codigoProduto);

                    List<Produtos> produtos = await GetProdutosResultSet(command);
                    int estoqueAtual = produtos[0].estoque + consumo.quantidade;

                    sql = @"UPDATE produtos SET estoque = @estoque, dtAlteracao = @dtAlteracao WHERE codigo = @codigo;";

                    command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@estoque", estoqueAtual);
                    command.Parameters.AddWithValue("@dtAlteracao", consumo.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", consumo.codigoProduto);

                    await command.ExecuteScalarAsync();

                    sql = @"DELETE FROM consumos WHERE codigo = @codigo;";

                    command = new NpgsqlCommand(sql, conexao);
                    command.Parameters.AddWithValue("@codigo", consumo.codigo);

                    var result = await command.ExecuteNonQueryAsync();

                    transaction.Commit();
                    return result == 1 ? true : false;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Não é foi possível excluir o Consumo");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Consumos>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
