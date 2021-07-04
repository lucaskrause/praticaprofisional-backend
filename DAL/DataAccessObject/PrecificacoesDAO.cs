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
                    string sql = @"SELECT * FROM precificacoes WHERE status = 'Ativo' ORDER BY minpessoas;";

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
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        throw new Exception("Precificação não encontrada");
                    }
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
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "precificacoes", "minpessoas", preco.minPessoas);
                    if (exists)
                    {
                        exists = await CheckExist(conexao, "precificacoes", "maxpessoas", preco.maxPessoas);
                        if (exists)
                        {
                            string sql = @"INSERT INTO precificacoes(minpessoas, maxpessoas, valor, dtcadastro, dtalteracao, status) VALUES (@minPessoas, @maxPessoas, @valor, @dtCadastro, @dtAlteracao, @status) returning codigo;";

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
                        else
                        {
                            throw new Exception("Precificação com máximo de pessoas igual a " + preco.maxPessoas + " já cadastra");
                        }
                     }
                    else
                    {
                        throw new Exception("Precificação com mínimo de pessoas igual a " + preco.minPessoas + " já cadastra");
                    }
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
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "precificacoes", "minpessoas", preco.minPessoas, preco.codigo);
                    if (exists)
                    {
                        exists = await CheckExist(conexao, "precificacoes", "maxpessoas", preco.maxPessoas, preco.codigo);
                        if (exists)
                        {
                            string sql = @"UPDATE precificacoes SET minpessoas = @minPessoas, maxpessoas = @maxPessoas, valor = @valor, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                            command.Parameters.AddWithValue("@minPessoas", preco.minPessoas);
                            command.Parameters.AddWithValue("@maxPessoas", preco.maxPessoas);
                            command.Parameters.AddWithValue("@valor", preco.valor);
                            command.Parameters.AddWithValue("@dtAlteracao", preco.dtCadastro);
                            command.Parameters.AddWithValue("@codigo", preco.codigo);

                            await command.ExecuteNonQueryAsync();
                            return preco;
                        }
                        else
                        {
                            throw new Exception("Precificação com máximo de pessoas igual a " + preco.maxPessoas + " já cadastra");
                        }
                    }
                    else
                    {
                        throw new Exception("Precificação com mínimo de pessoas igual a " + preco.minPessoas + " já cadastra");
                    }
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
                    string sql = @"DELETE FROM precificacoes WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);
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
