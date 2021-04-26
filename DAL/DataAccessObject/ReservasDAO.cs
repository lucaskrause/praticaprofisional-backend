using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ReservasDAO : DAO<Reservas>
    {
        public override Task<Reservas> BuscarPorID(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<Reservas> Editar(Reservas reserva)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(Reservas reserva)
        {
            throw new NotImplementedException();
        }

        public override Task<Reservas> Inserir(Reservas reserva)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Reservas>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Reservas>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
