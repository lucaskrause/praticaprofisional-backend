using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class PrecificacoesDAO : DAO<Precificacoes>
    {
        public override async Task<IList<Precificacoes>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM precificacoes WHERE status = 'Ativo' ORDER BY codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Precificacoes> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Precificacoes> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM precificacoes WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Precificacoes> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Precificacoes> Inserir(Precificacoes preco)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"INSERT INTO precificacoes(minpessoas, maxpessoas, valor, dtcadastro, dtalteracao, status) VALUES (@minPessoas, @maxPessoas, @valor, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@minPessoas", preco.minPessoas);
                    command.Parameters.AddWithValue("@maxPessoas", preco.maxPessoas);
                    command.Parameters.AddWithValue("@valor", preco.valor);
                    command.Parameters.AddWithValue("@dtCadastro", preco.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", preco.dtCadastro);
                    command.Parameters.AddWithValue("@status", preco.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    preco.codigo = (int)idInserido;
                    return preco;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Precificacoes> Editar(Precificacoes preco)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE precificacoes SET minpessoas = @minPessoas, maxpessoas = @maxPessoas, valor = @valor, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@minPessoas", preco.minPessoas);
                    command.Parameters.AddWithValue("@maxPessoas", preco.maxPessoas);
                    command.Parameters.AddWithValue("@valor", preco.valor);
                    command.Parameters.AddWithValue("@dtAlteracao", preco.dtCadastro);
                    command.Parameters.AddWithValue("@codigo", preco.codigo);

                    await command.ExecuteNonQueryAsync();
                    return preco;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Precificacoes preco)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE precificacoes SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM paises WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", preco.status);
                    command.Parameters.AddWithValue("@dtAlteracao", preco.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", preco.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Precificacoes>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
