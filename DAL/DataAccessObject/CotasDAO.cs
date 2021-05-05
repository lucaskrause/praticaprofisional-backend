using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class CotasDAO : DAO<Cotas>
    {
        public override Task<Cotas> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public override Task<Cotas> Editar(Cotas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(Cotas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Cotas> Inserir(Cotas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Cotas>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Cotas>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
