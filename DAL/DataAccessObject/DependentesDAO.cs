using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class DependentesDAO : DAO<Dependentes>
    {
        public override Task<IList<Dependentes>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<Dependentes> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public override Task<Dependentes> Inserir(Dependentes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Dependentes> Editar(Dependentes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(Dependentes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Dependentes>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
