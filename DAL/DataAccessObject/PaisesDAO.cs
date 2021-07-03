using Npgsql;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class PaisesDAO : DAO<Paises>
    {
        public PaisesDAO() : base()
        {
        }

        public async Task<bool> checkPais(NpgsqlConnection conexao, Paises pais)
        {
            string sql = @"SELECT * FROM paises WHERE pais = @pais;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@pais", pais.pais);

            List<Paises> list = await GetResultSet(command);

            if(list.Count > 0)
            {
                return false;
            }
            return true;
        }

        public override async Task<IList<Paises>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM paises WHERE status = 'Ativo' ORDER BY codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Paises> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Paises> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM paises WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Paises> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    } else
                    {
                        throw new Exception("País não encontrado");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Paises> Inserir(Paises pais)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await checkPais(conexao, pais);
                    if (exists)
                    {
                        string sql = @"INSERT INTO paises(pais, sigla, ddi, dtCadastro, dtAlteracao, status) VALUES (@pais, @sigla, @ddi, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@pais", pais.pais);
                        command.Parameters.AddWithValue("@sigla", pais.sigla);
                        command.Parameters.AddWithValue("@ddi", pais.ddi);
                        command.Parameters.AddWithValue("@dtCadastro", pais.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", pais.dtAlteracao);
                        command.Parameters.AddWithValue("@status", pais.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        pais.codigo = (int)idInserido;
                        return pais;
                    } else
                    {
                        throw new Exception("País já existente");
                    }
                    
                }
                finally
                {
                    conexao.Close();
                }            
            }
        }

        public override async Task<Paises> Editar(Paises pais)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE paises SET pais = @pais, sigla = @sigla, ddi = @ddi, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@pais", pais.pais);
                    command.Parameters.AddWithValue("@sigla", pais.sigla);
                    command.Parameters.AddWithValue("@ddi", pais.ddi);
                    command.Parameters.AddWithValue("@dtAlteracao", pais.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", pais.codigo);

                    await command.ExecuteNonQueryAsync();
                    return pais;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Paises pais)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM paises WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", pais.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE paises SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", pais.status);
                    command.Parameters.AddWithValue("@dtAlteracao", pais.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", pais.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Paises>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
