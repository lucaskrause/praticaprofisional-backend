using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public abstract class DAO<T> where T : AbstractEntity
    {
        private readonly MySqlConnection _conexao;

        public DAO()
        {
            string strConn = "server=localhost;user=root;password=;database=praticaprofissional";
            _conexao = new MySqlConnection(strConn);
        }

        public MySqlConnection GetCurrentConnection() => _conexao;

        /* protected MySqlConnection CreateCommandTransaction(MySqlConnection transaction)
        {
            MySqlConnection comando = this._conexao.CreateCommand();

            comando.Transaction = transaction;

            return comando;
        } */

        public abstract Task<T> Inserir(T entity);

        public abstract Task<T> Editar(T entity);

        public abstract Task<bool> Excluir(T entity);

        public abstract  Task<T> BuscarPorID(T entity);

        public abstract Task<IList<T>> ListarTodos();

        public abstract Task<T> Pesquisar(string str);

    }
}
