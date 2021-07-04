using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class EstadosService : IService<Estados>
    {
        private readonly EstadosDAO estadosDao = null;

        public EstadosService() => this.estadosDao = new EstadosDAO();

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
            string error = estado.Validation();
            if (error == null) {
                estado.PrepareSave();
                estado.Ativar();
                return await estadosDao.Inserir(estado);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Estados> Editar(Estados estado)
        {
            string error = estado.Validation();
            if (error == null)
            {
                estado.PrepareSave();
                return await estadosDao.Editar(estado);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Estados estado = new Estados();
            estado.codigo = codigo;
            estado.PrepareSave();
            estado.Inativar();
            return await estadosDao.Excluir(estado);
        }

        public async Task<IList<Estados>> Pesquisar(string str)
        {
            return await estadosDao.Pesquisar(str);
        }
    }
}
