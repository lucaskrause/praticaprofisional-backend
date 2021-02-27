using DAL.DataAccessObject;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class Service<T> where T : AbstractEntity
    {
        protected readonly DAO Dao;

        public Service(DAO dao)
        {
            Dao = dao;
        }

        public virtual void Inserir(T entity)
        {
            Dao.Inserir(entity);
        }

        public virtual void Editar(T entity)
        {
            Dao.Editar(entity);
        }

        public virtual void Excluir(T entity)
        {
            Dao.Excluir(entity);
        }

        public virtual object BuscarPorID(T entity)
        {
            return this.Dao.BuscarPorID(entity);
        }

        public virtual DataTable ListarTodos()
        {
            return Dao.ListarTodos();
        }

        public virtual object Pesquisar(string obj)
        {
            return Dao.Pesquisar(obj);
        }
    }
}
