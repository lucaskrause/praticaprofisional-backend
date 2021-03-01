using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Service
{
    public interface IService<T> where T : AbstractEntity
    {
        public abstract T Inserir(T entity);

        public abstract T Editar(T entity);

        public abstract bool Excluir(T entity);

        public abstract T BuscarPorID(T entity);

        public abstract IList<T> ListarTodos();

        public abstract IList<T> Pesquisar(string str);
    }
}
