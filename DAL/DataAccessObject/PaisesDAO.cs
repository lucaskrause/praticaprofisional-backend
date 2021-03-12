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
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();

                try
                {
                    string sql = @"INSERT INTO paises(pais, sigla, ddi, dtCadastro, dtAlteracao, status) VALUES (@pais, @sigla, @ddi, @dtCadastro, @dtAlteracao, @status)";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@pais", pais.Pais);
                    command.Parameters.AddWithValue("@sigla", pais.Sigla);
                    command.Parameters.AddWithValue("@ddi", pais.DDI);
                    command.Parameters.AddWithValue("@dtCadastro", pais.DtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", pais.DtAlteracao);
                    command.Parameters.AddWithValue("@status", pais.Status);
                    
                    await command.ExecuteNonQueryAsync();
                    return pais;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    transaction.Commit();
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

                    command.Parameters.AddWithValue("@pais", pais.Pais);
                    command.Parameters.AddWithValue("@sigla", pais.Sigla);
                    command.Parameters.AddWithValue("@ddi", pais.DDI);
                    command.Parameters.AddWithValue("@dtAlteracao", pais.DtAlteracao);
                    command.Parameters.AddWithValue("@codigo", pais.Codigo);

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
                    command.Parameters.AddWithValue("@dtAlteracao", pais.DtAlteracao);
                    command.Parameters.AddWithValue("@codigo", pais.Codigo);

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
