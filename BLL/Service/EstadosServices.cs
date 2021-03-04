using DAL.DataAccessObject;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class EstadosServices : IService<Estados>
    {
        private readonly EstadosDAO estadosDao = null;

        public EstadosServices() => this.estadosDao = new EstadosDAO();

        public async Task<IList<Estados>> ListarTodos()
        {
            return await estadosDao.ListarTodos();
        }

        public async Task<Estados> BuscarPorID(int codigo)
        {
            return await estadosDao.BuscarPorID(codigo);
        }

        public async Task<Estados> Inserir(Estados estado)
        {
            estado.PrepareSave();
            estado.Ativar();
            return await estadosDao.Inserir(estado);
        }

        public async Task<Estados> Editar(Estados estado)
        {
            estado.PrepareSave();
            return await estadosDao.Editar(estado);
        }

        public async Task<bool> Excluir(Estados estado)
        {
            estado.PrepareSave();
            estado.Inativar();
            return await estadosDao.Excluir(estado);
        }

        public Task<IList<Estados>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
