using MySqlConnector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public override async Task<Paises> BuscarPorID(Paises entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<Paises> Editar(Paises obj)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Excluir(Paises entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<Paises> Inserir(Paises pais)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"INSERT INTO paises(pais, sigla, dtCadastro, dtAlteracao, status) values (@pais, @sigla, @dtCadastro, @dtAlteracao, @status)";

                    conexao.Open();

                    MySqlCommand command = new MySqlCommand(sql, conexao);

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

        public override async Task<IList<Paises>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM paises";

                    conexao.Open();

                    MySqlCommand command = new MySqlCommand(sql, conexao);
                    
                    command.ExecuteNonQuery();

                    List<Paises> list = new List<Paises>();

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
                        Paises p = JsonConvert.DeserializeObject<Paises>(stringJson);
                        list.Add(p);
                    }
                    return list;
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

        public override async Task<Paises> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
