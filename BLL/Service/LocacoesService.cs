using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class LocacoesService : IService<Locacoes>
    {
        private readonly LocacoesDAO locacoesDao = null;

        public LocacoesService() => this.locacoesDao = new LocacoesDAO();

        public async Task<IList<Locacoes>> ListarTodos()
        {
            return await locacoesDao.ListarTodos();
        }

        public async Task<Locacoes> BuscarPorID(int codigo)
        {
            return await locacoesDao.BuscarPorID(codigo);
        }

        public async Task<Locacoes> Inserir(Locacoes reserva)
        {
            string error = reserva.Validation();
            if (error == null) {
                reserva.PrepareSave();
                reserva.Ativar();
                return await locacoesDao.Inserir(reserva);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Locacoes> Editar(Locacoes reserva)
        {
            string error = reserva.Validation();
            if (error == null)
            {
                reserva.PrepareSave();
                return await locacoesDao.Editar(reserva);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Locacoes reserva = new Locacoes();
            reserva.codigo = codigo;
            reserva.PrepareSave();
            reserva.Inativar();
            return await locacoesDao.Excluir(reserva);
        }

        public async Task<IList<Locacoes>> Pesquisar(string str)
        {
            return await locacoesDao.Pesquisar(str);
        }
    }
}
