using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ComprasService : IService<Compras>
    {
        private readonly ComprasDAO comprasDao = null;

        public ComprasService() => this.comprasDao = new ComprasDAO();

        public async Task<IList<Compras>> ListarTodos()
        {
            return await comprasDao.ListarTodos();
        }

        public async Task<Compras> BuscarPorID(int id)
        {
            return await comprasDao.BuscarPorID(id);
        }

        public async Task<Compras> Inserir(Compras compra)
        {
            string error = compra.Validation();
            if (error == null)
            {
                compra.PrepareSave();
                compra.Ativar();
                return await comprasDao.Inserir(compra);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<Compras> Editar(Compras compra)
        {
            string error = compra.Validation();
            if (error == null)
            {
                compra.PrepareSave();
                return await comprasDao.Editar(compra);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Compras compra = new Compras();
            compra.codigo = codigo;
            compra.PrepareSave();
            compra.Inativar();
            return await comprasDao.Excluir(compra);
        }

        public async Task<IList<Compras>> Pesquisar(string str)
        {
            return await comprasDao.Pesquisar(str);
        }
    }
}
