using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ReservasService : IService<Reservas>
    {
        private readonly ReservasDAO reservasDao = null;

        public ReservasService() => this.reservasDao = new ReservasDAO();

        public async Task<IList<Reservas>> ListarTodos()
        {
            return await reservasDao.ListarTodos();
        }

        public async Task<Reservas> BuscarPorID(int codigo)
        {
            return await reservasDao.BuscarPorID(codigo);
        }

        public async Task<Reservas> Inserir(Reservas reserva)
        {
            string error = reserva.Validation();
            if (error == null) {
                reserva.codigoEmpresa = 1;
                reserva.PrepareSave();
                reserva.Ativar();
                return await reservasDao.Inserir(reserva);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Reservas> Editar(Reservas reserva)
        {
            string error = reserva.Validation();
            if (error == null)
            {
                reserva.PrepareSave();
                return await reservasDao.Editar(reserva);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Reservas reserva = new Reservas();
            reserva.codigo = codigo;
            reserva.PrepareSave();
            reserva.Inativar();
            return await reservasDao.Excluir(reserva);
        }

        public async Task<IList<Reservas>> Pesquisar(string str)
        {
            return await reservasDao.Pesquisar(str);
        }
    }
}
