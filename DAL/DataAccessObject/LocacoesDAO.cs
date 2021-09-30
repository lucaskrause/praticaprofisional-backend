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
        public async Task<List<AreasReservas>> GetAreasReservasResultSet(NpgsqlCommand command)
        {
            List<AreasReservas> list = new List<AreasReservas>();

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
                AreasReservas p = JsonConvert.DeserializeObject<AreasReservas>(stringJson);
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

        public async Task<List<AreasLocacao>> GetAreasReservas(NpgsqlConnection conexao, int codigoReserva)
        {
            string sql = @"SELECT * FROM areasreservas WHERE codigoreserva = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoReserva);

            List<AreasReservas> listAreasReservas = await GetAreasReservasResultSet(command);

            if (listAreasReservas.Count > 0)
            {
                List<AreasLocacao> areasLocacao = new List<AreasLocacao>();
                for (int i = 0; i < listAreasReservas.Count; i++)
                {
                    sql = @"SELECT * FROM areasLocacao WHERE codigo = @codigo;";

                    command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", listAreasReservas[i].codigoArea);

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

        public async Task<bool> InsertAreasReservas(NpgsqlConnection conexao, List<AreasLocacao> areasLocacao, int codigoReserva)
        {
            for (int i = 0; i < areasLocacao.Count; i++)
            {
                AreasLocacao area = areasLocacao[i];
                area.Ativar();
                area.PrepareSave();

                string sql = @"INSERT INTO areasreservas(codigoreserva, codigoarea) VALUES (@codigoReserva, @codigoArea);";

                NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                command.Parameters.AddWithValue("@codigoReserva", codigoReserva);
                command.Parameters.AddWithValue("@codigoArea", area.codigo);

                await command.ExecuteNonQueryAsync();
            }
            return true;
        }

        public async Task<bool> DeleteAreasReservas(NpgsqlConnection conexao, int codigoReserva)
        {
            string sql = @"DELETE FROM areasreservas WHERE codigoreserva = @codigo";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoReserva);

            var result = await command.ExecuteNonQueryAsync();
            return result == 1 ? true : false;
        }

        public override async Task<IList<Locacoes>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT reservas.codigo, reservas.codigocliente, reservas.qtdepessoas, reservas.dtreserva, reservas.valor, reservas.codigocondicaopagamento, reservas.dtcadastro, reservas.dtalteracao, reservas.status, clientes.nome as nomeCliente, condicoespagamento.descricao AS nomeCondicao FROM reservas INNER JOIN clientes ON(reservas.codigocliente = clientes.codigo) INNER JOIN condicoespagamento ON (reservas.codigocondicaopagamento = condicoespagamento.codigo) WHERE reservas.status = 'Ativo' ORDER BY reservas.codigo;";

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
                    string sql = @"SELECT reservas.codigo, reservas.codigocliente, reservas.qtdepessoas, reservas.dtreserva, reservas.valor, reservas.codigocondicaopagamento, reservas.dtcadastro, reservas.dtalteracao, reservas.status, clientes.nome as nomeCliente, condicoespagamento.descricao AS nomeCondicao FROM reservas INNER JOIN clientes ON(reservas.codigocliente = clientes.codigo) INNER JOIN condicoespagamento ON (reservas.codigocondicaopagamento = condicoespagamento.codigo) WHERE reservas.codigo = @codigo AND reservas.status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Locacoes> listReserva = await GetResultSet(command);

                    if (listReserva.Count > 0)
                    {
                        listReserva[0].areasLocacao = await GetAreasReservas(conexao, codigo);
                    }
                    else
                    {
                        throw new Exception("Reserva não encontrada");
                    }

                    transaction.Commit();
                    return listReserva[0];
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

        public override async Task<Locacoes> Inserir(Locacoes reserva)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"INSERT INTO reservas(codigocliente, qtdepessoas, dtreserva, valor, codigocondicaopagamento, dtcadastro, dtalteracao, status) VALUES (@codigoCliente, @qtdePessoas, @dtReserva, @valor, @codigoCondicaoPagamento, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoCliente", reserva.codigoCliente);
                    command.Parameters.AddWithValue("@qtdePessoas", reserva.qtdePessoas);
                    command.Parameters.AddWithValue("@dtReserva", reserva.dtReserva);
                    command.Parameters.AddWithValue("@valor", reserva.valor);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", reserva.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtCadastro", reserva.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", reserva.dtAlteracao);
                    command.Parameters.AddWithValue("@status", reserva.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    reserva.codigo = (int)idInserido;

                    int qtdAreasLocacao = reserva.areasLocacao.Count;
                    if (qtdAreasLocacao > 0)
                    {
                        await InsertAreasReservas(conexao, reserva.areasLocacao, reserva.codigo);
                    }

                    transaction.Commit();
                    return reserva;
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

        public override async Task<Locacoes> Editar(Locacoes reserva)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"UPDATE reservas SET codigocliente = @codigoCliente, qtdepessoas = @qtdePessoas, dtreserva = @dtReserva , valor = @valor, codigocondicaopagamento = @codigoCondicaoPagamento, dtalteracao = @dtAlteracao WHERE codigo = @codigo AND status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoCliente", reserva.codigoCliente);
                    command.Parameters.AddWithValue("@qtdePessoas", reserva.qtdePessoas);
                    command.Parameters.AddWithValue("@dtReserva", reserva.dtReserva);
                    command.Parameters.AddWithValue("@valor", reserva.valor);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", reserva.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtAlteracao", reserva.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", reserva.codigo);

                    await command.ExecuteNonQueryAsync();

                    await DeleteAreasReservas(conexao, reserva.codigo);

                    int qtdAreasLocacao = reserva.areasLocacao.Count;
                    if (qtdAreasLocacao > 0)
                    {
                        await InsertAreasReservas(conexao, reserva.areasLocacao, reserva.codigo);
                    }

                    transaction.Commit();
                    return reserva;
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

        public override async Task<bool> Excluir(Locacoes reserva)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM reservas WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", reserva.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    throw new Exception("Não foi possivel excluir a reserva");
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
