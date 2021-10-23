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
                return null;
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

        public override async Task<IList<Locacoes>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT locacoes.codigo, locacoes.codigocliente, locacoes.qtdepessoas, locacoes.dtlocacao, locacoes.valor, locacoes.codigocondicaopagamento, locacoes.dtcadastro, locacoes.dtalteracao, locacoes.status, clientes.nome as nomeCliente, condicoespagamento.descricao AS nomeCondicao FROM locacoes INNER JOIN clientes ON(locacoes.codigocliente = clientes.codigo) INNER JOIN condicoespagamento ON (locacoes.codigocondicaopagamento = condicoespagamento.codigo) WHERE locacoes.status = 'Ativo' ORDER BY locacoes.codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Locacoes> list = await GetResultSet(command);
                    return list;
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
                try
                {
                    string sql = @"DELETE FROM locacoes WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", locacao.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    throw new Exception("Não foi possivel excluir a locacao");
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
