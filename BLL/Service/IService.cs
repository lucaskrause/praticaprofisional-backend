using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IService<T> where T : AbstractEntity
    {
        public abstract Task<T> Inserir(T entity);

        public abstract Task<T> Editar(T entity);

        public abstract Task<bool> Excluir(T entity);

        public abstract Task<T> BuscarPorID(int id);

        public abstract Task<IList<T>> ListarTodos();

        public abstract Task<IList<T>> Pesquisar(string str);
    }
}
