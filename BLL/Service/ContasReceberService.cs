using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ContasReceberService : IService<ContasReceber>
    {
        private readonly ContasReceberDAO contasReceberDao = null;

        public ContasReceberService() => this.contasReceberDao = new ContasReceberDAO();

        public async Task<IList<ContasReceber>> ListarTodos()
        {
            return await contasReceberDao.ListarTodos();
        }

        public async Task<ContasReceber> BuscarPorID(int id)
        {
            return await contasReceberDao.BuscarPorID(id);
        }

        public async Task<ContasReceber> BuscarParcela(ContasReceber contaReceber)
        {
            return await contasReceberDao.BuscarParcela(contaReceber);
        }

        public async Task<ContasReceber> Inserir(ContasReceber contaReceber)
        {
            string error = contaReceber.Validation();
            if (error == null)
            {
                contaReceber.dtEmissao = DateTime.Now;
                contaReceber.pendente();
                return await contasReceberDao.Inserir(contaReceber);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<ContasReceber> Editar(ContasReceber contaReceber)
        {
            string error = contaReceber.Validation();
            if (error == null)
            {
                return await contasReceberDao.Editar(contaReceber);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<ContasReceber> Receber(ContasReceber contaReceber)
        {
            contaReceber.dtPagamento = DateTime.Now;
            contaReceber.pagar();
            return await contasReceberDao.Receber(contaReceber);
        }

        public async Task<bool> Excluir(int codigo)
        {
            return false;
        }

        public async Task<IList<ContasReceber>> Pesquisar(string str)
        {
            return await contasReceberDao.Pesquisar(str);
        }
    }
}
