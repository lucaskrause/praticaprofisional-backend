using DAL.DataAccessObject;
using RUPsystem.Entities;
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
        
        public async Task<Paises> BuscarPorID(Paises entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Paises> Inserir(Paises pais)
        {
            pais.PrepareSave();
            pais.Ativar();
            return await paisesDao.Inserir(pais);
        }

        public async Task<Paises> Editar(Paises entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Excluir(Paises entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Paises>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
