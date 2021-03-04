using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DAL.DataAccessObject
{
    public abstract class DAO<T> where T : AbstractEntity
    {
        private readonly NpgsqlConnection _conexao;

        public DAO()
        {
            string strConn = "Server=localhost;Port=5432;Database=praticaprofissional;User Id=postgres;Password=1234;";
            //"server=localhost;user=root;password=;database=praticaprofissional";
            _conexao = new NpgsqlConnection(strConn);
        }

        public NpgsqlConnection GetCurrentConnection() => _conexao;

        /* protected MySqlConnection CreateCommandTransaction(MySqlConnection transaction)
        {
            MySqlConnection comando = this._conexao.CreateCommand();

            comando.Transaction = transaction;

            return comando;
        } */

        public async Task<List<T>> GetResultSet(NpgsqlCommand command)
        {
            List<T> list =  new List<T>();

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
                T p = JsonConvert.DeserializeObject<T>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public abstract Task<IList<T>> ListarTodos();

        public abstract Task<T> BuscarPorID(int id);

        public abstract Task<T> Inserir(T entity);

        public abstract Task<T> Editar(T entity);

        public abstract Task<bool> Excluir(T entity);

        public abstract Task<T> Pesquisar(string str);

    }
}
