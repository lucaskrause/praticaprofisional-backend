using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class PaisesDAO : DAO<Paises>
    {
        public PaisesDAO() : base()
        {
        }

        public override async Task<IList<Paises>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM paises WHERE status = 'Ativo'";

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

                    return list[0];
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
                    string sql = @"INSERT INTO paises(pais, sigla, dtCadastro, dtAlteracao, status) VALUES (@pais, @sigla, @dtCadastro, @dtAlteracao, @status)";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@pais", pais.Pais);
                    command.Parameters.AddWithValue("@sigla", pais.Sigla);
                    command.Parameters.AddWithValue("@dtCadastro", pais.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", pais.dtAlteracao);
                    command.Parameters.AddWithValue("@status", pais.Status);
                    
                    await command.ExecuteNonQueryAsync();
                    return pais;
                }
                catch
                {
                    throw;
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
                    string sql = @"UPDATE paises SET pais = @pais, sigla = @sigla, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@pais", pais.Pais);
                    command.Parameters.AddWithValue("@sigla", pais.Sigla);
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
                    string sql = @"UPDATE paises SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM paises WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", pais.Status);
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

        public override Task<Paises> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
