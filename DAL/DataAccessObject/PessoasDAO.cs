using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class PessoasDAO : DAO<Pessoas>
    {
        public override Task<Pessoas> BuscarPorID(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<Pessoas> Editar(Pessoas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(Pessoas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Pessoas> Inserir(Pessoas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Pessoas>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Pessoas>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
