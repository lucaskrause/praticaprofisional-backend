using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ReservasDAO : DAO<Reservas>
    {
        public override async Task<IList<Reservas>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Reservas> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Reservas> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Reservas> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Reservas> Inserir(Reservas reserva)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoEmpresa", reserva.codigoEmpresa);
                    command.Parameters.AddWithValue("@codigoCliente", reserva.codigoCliente);
                    command.Parameters.AddWithValue("@codigoPreco", reserva.codigoPreco);
                    command.Parameters.AddWithValue("@qtdePessoas", reserva.qtdePessoas);
                    command.Parameters.AddWithValue("@DtReserva", reserva.DtReserva);
                    command.Parameters.AddWithValue("@dtCadastro", reserva.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", reserva.dtAlteracao);
                    command.Parameters.AddWithValue("@status", reserva.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    reserva.codigo = (int)idInserido;
                    return reserva;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Reservas> Editar(Reservas reserva)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoCliente", reserva.codigoCliente);
                    command.Parameters.AddWithValue("@codigoPreco", reserva.codigoPreco);
                    command.Parameters.AddWithValue("@qtdePessoas", reserva.qtdePessoas);
                    command.Parameters.AddWithValue("@DtReserva", reserva.DtReserva);
                    command.Parameters.AddWithValue("@dtAlteracao", reserva.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return reserva;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Reservas reserva)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE reservas SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM reservas WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", reserva.status);
                    command.Parameters.AddWithValue("@dtAlteracao", reserva.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", reserva.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Reservas>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
