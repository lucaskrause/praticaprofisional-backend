using DAL.DataAccessObject;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
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
            entity.PrepareSave();
            entity.Ativar();
            Dao.Inserir(entity);
        }
    }
}
