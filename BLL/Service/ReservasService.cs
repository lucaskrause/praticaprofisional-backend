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

        public string validaReserva(Reservas reserva)
        {
            if (reserva.codigoCliente <= 0)
            {
                return "Cliente obrigatório";
            }
            else if (reserva.qtdePessoas <= 0)
            {
                return "Quantidade de Pessoas obrigatória";
            }
            else if (reserva.dtReserva == null || reserva.dtReserva.Date < (DateTime.Now).Date)
            {
                return "Data da Reserva obrigatória";
            } 
            else if (reserva.valor <= 0)
            {
                return "Data da Reserva obrigatória";
            }
            else if (reserva.codigoCondicaoPagamento <= 0)
            {
                return "Condição de Pagamento obrigatório";
            }
            else if (reserva.areasLocacao == null || reserva.areasLocacao.Count == 0)
            {
                return "Áreas de Locação obrigatório";
            } 
            else
            {
                return null;
            }
        }

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
            string error = validaReserva(reserva);
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
            string error = validaReserva(reserva);
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
