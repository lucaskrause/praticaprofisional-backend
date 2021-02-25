using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class AbstractService<T> where T : AbstractEntity
    {

        public AbstractService()
        {

        }

        public virtual async Task<T> Save(T entity)
        {
            entity.PrepareSave();

            entity.Ativar();

            //_repository.Add(entity);

            //await _repository.SaveModificationsAsync();

            return entity;
        }
    }
}
