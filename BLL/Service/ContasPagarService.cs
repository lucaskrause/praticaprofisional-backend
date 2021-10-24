using DAL.Models;
using DAL.DataAccessObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ContasPagarService : IService<ContasPagar>
    {
        private readonly ContasPagarDAO contasPagarDao = null;

        public ContasPagarService() => this.contasPagarDao = new ContasPagarDAO();

        public async Task<IList<ContasPagar>> ListarTodos()
        {
            return await contasPagarDao.ListarTodos();
        }

        public async Task<ContasPagar> BuscarPorID(int id)
        {
            return await contasPagarDao.BuscarPorID(id);
        }

        public async Task<ContasPagar> BuscarParcela(ContasPagar contaPagar)
        {
            return await contasPagarDao.BuscarParcela(contaPagar);
        }

        public async Task<ContasPagar> Inserir(ContasPagar contaPagar)
        {
            string error = contaPagar.Validation();
            if (error == null)
            {
                contaPagar.dtEmissao = new DateTime();
                contaPagar.pendente();
                return await contasPagarDao.Inserir(contaPagar);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<ContasPagar> Editar(ContasPagar contaPagar)
        {
            string error = contaPagar.Validation();
            if (error == null)
            {
                return await contasPagarDao.Editar(contaPagar);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            return false;
        }

        public async Task<IList<ContasPagar>> Pesquisar(string str)
        {
            return await contasPagarDao.Pesquisar(str);
        }
    }
}
