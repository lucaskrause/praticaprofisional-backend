using Npgsql;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class EstadosDAO : DAO<Estados>
    {
        public EstadosDAO() : base()
        {
        }

        public override async Task<IList<Estados>> ListarTodos()
        {
            using(var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT estados.codigo, estados.estado, estados.uf, paises.pais, estados.dtCadastro, estados.dtAlteracao, paises.pais as nomePais FROM estados INNER JOIN paises ON (estados.codigopais = paises.codigo) WHERE estados.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Estados> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Estados> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM estados WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Estados> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Estados> Inserir(Estados estado)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"INSERT INTO estados (estado, uf, codigoPais, dtCadastro, dtAlteracao, status) VALUES (@estado, @uf, @codigoPais, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@estado", estado.estado);
                    command.Parameters.AddWithValue("@uf", estado.uf);
                    command.Parameters.AddWithValue("@codigoPais", estado.codigoPais);
                    command.Parameters.AddWithValue("@dtCadastro", estado.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", estado.dtAlteracao);
                    command.Parameters.AddWithValue("@status", estado.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    estado.codigo = (int) idInserido;
                    return estado;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Estados> Editar(Estados estado)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE estados SET estado = @estado, uf = @uf, codigoPais = @codigoPais, dtAlteracao = @dtAlteracao WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@estado", estado.estado);
                    command.Parameters.AddWithValue("@uf", estado.uf);
                    command.Parameters.AddWithValue("@codigoPais", estado.codigoPais);
                    command.Parameters.AddWithValue("@dtAlteracao", estado.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", estado.codigo);

                    await command.ExecuteNonQueryAsync();
                    return estado;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Estados estado)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE estados SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM estados WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", estado.status);
                    command.Parameters.AddWithValue("@dtAlteracao", estado.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", estado.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Estados>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
