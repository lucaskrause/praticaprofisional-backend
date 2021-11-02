using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IService<T> where T : Pai
    {
        public abstract Task<IList<T>> ListarTodos();

        public abstract Task<T> BuscarPorID(int codigo);

        public abstract Task<T> Inserir(T entity);

        public abstract Task<T> Editar(T entity);

        public abstract Task<bool> Excluir(int codigo);

        public abstract Task<IList<T>> Pesquisar(string str);
    }
}
