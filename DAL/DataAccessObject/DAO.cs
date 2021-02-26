using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL.DataAccessObject
{
    public class DAO
    {
        private SqlConnection _conexao;

        public DAO()
        {
            string strConn = "Server=localhost;Database=praticaprofissional;Uid=root;Pwd=;";
            SqlConnection conexao = new SqlConnection(strConn);
        }

        public SqlConnection GetCurrentConnection() => _conexao;

        public virtual void Inserir(Object obj)
        {

        }

        public virtual void Editar(Object obj)
        {

        }

        public virtual void Excluir(Object obj)
        {

        }

        public virtual object BuscarPorID(Object obj)
        {
            return null;
        }

        public virtual DataTable ListarTodos()
        {
            return null;
        }

        public virtual object Pesquisar(string obj)
        {
            return null;
        }

        protected SqlCommand CreateCommandTransaction(SqlTransaction transaction)
        {
            SqlCommand comando = this._conexao.CreateCommand();

            comando.Transaction = transaction;

            return comando;
        }
    }
}
