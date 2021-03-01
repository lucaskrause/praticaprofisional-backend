using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL.DataAccessObject
{
    public abstract class DAO<T> where T : AbstractEntity
    {
        private SqlConnection _conexao;

        public DAO()
        {
            string strConn = "Server=localhost;Database=praticaprofissional;Uid=root;Pwd=;";
            _conexao = new SqlConnection(strConn);
        }

        public SqlConnection GetCurrentConnection() => _conexao;

        protected SqlCommand CreateCommandTransaction(SqlTransaction transaction)
        {
            SqlCommand comando = this._conexao.CreateCommand();

            comando.Transaction = transaction;

            return comando;
        }

        public abstract T Inserir(T entity);

        public abstract T Editar(T entity);

        public abstract bool Excluir(T entity);

        public abstract T BuscarPorID(T entity);

        public abstract IList<T> ListarTodos();

        public abstract T Pesquisar(string str);

    }
}
