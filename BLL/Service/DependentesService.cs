using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class DependentesService : IService<Dependentes>
    {
        private readonly DependentesDAO dependentesDao = null;

        public DependentesService() => this.dependentesDao = new DependentesDAO();

        public async Task<IList<Dependentes>> ListarTodos()
        {
            return await dependentesDao.ListarTodos();
        }

        public async Task<Dependentes> BuscarPorID(int codigo)
        {
            return await dependentesDao.BuscarPorID(codigo);
        }

        public async Task<Dependentes> Inserir(Dependentes dependente)
        {
            string error = dependente.Validation();
            if (error == null)
            {
                dependente.Ativar();
                dependente.PrepareSave();
                return await dependentesDao.Inserir(dependente);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<Dependentes> Editar(Dependentes dependente)
        {
            string error = dependente.Validation();
            if (error == null)
            {
                dependente.PrepareSave();
                return await dependentesDao.Editar(dependente);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Dependentes dependente = new Dependentes();
            dependente.codigo = codigo;
            dependente.Inativar();
            dependente.PrepareSave();
            return await dependentesDao.Excluir(dependente);
        }

        public async Task<IList<Dependentes>> Pesquisar(string str)
        {
            return await dependentesDao.Pesquisar(str);
        }
    }
}
