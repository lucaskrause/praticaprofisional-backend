using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ClientesDAO : DAO<Clientes>
    {
        public override Task<Clientes> BuscarPorID(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<Clientes> Editar(Clientes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(Clientes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Clientes> Inserir(Clientes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Clientes>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Clientes>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
