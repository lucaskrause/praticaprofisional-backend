using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PaisesService : IService<Paises>
    {
        private readonly PaisesDAO paisesDao = null;

        public PaisesService() => this.paisesDao = new PaisesDAO();

        public async Task<IList<Paises>> ListarTodos()
        {
            return await paisesDao.ListarTodos();
        }
        
        public async Task<Paises> BuscarPorID(int id)
        {
            return await paisesDao.BuscarPorID(id);
        }

        public async Task<Paises> Inserir(Paises pais)
        {
            pais.PrepareSave();
            pais.Ativar();
            return await paisesDao.Inserir(pais);
        }

        public async Task<Paises> Editar(Paises pais)
        {
            pais.PrepareSave();
            return await paisesDao.Editar(pais);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Paises pais = new Paises();
            pais.codigo = codigo;
            pais.PrepareSave();
            pais.Inativar();
            return await paisesDao.Excluir(pais);
        }

        public async Task<IList<Paises>> Pesquisar(string str)
        {
            return await paisesDao.Pesquisar(str);
        }
    }
}
