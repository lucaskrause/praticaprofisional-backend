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

        public Task<ContasPagar> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<ContasPagar> BuscarParcela(ContasPagar contaPagar)
        {
            return await contasPagarDao.BuscarParcela(contaPagar);
        }

        public Task<ContasPagar> Inserir(ContasPagar entity)
        {
            throw new NotImplementedException();
        }

        public Task<ContasPagar> Editar(ContasPagar entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(int codigo)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ContasPagar>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
