using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CondicoesPagamentoService : IService<CondicoesPagamento>
    {
        private readonly CondicoesPagamentoDAO condicoesPagamentoDao = null;

        public CondicoesPagamentoService() => this.condicoesPagamentoDao = new CondicoesPagamentoDAO();

        public async Task<IList<CondicoesPagamento>> ListarTodos()
        {
            return await condicoesPagamentoDao.ListarTodos();
        }

        public async Task<CondicoesPagamento> BuscarPorID(int codigo)
        {
            return await condicoesPagamentoDao.BuscarPorID(codigo);
        }

        public async Task<CondicoesPagamento> Inserir(CondicoesPagamento condicaoPagamento)
        {
            string error = condicaoPagamento.Validation();
            if (error == null)
            {
                condicaoPagamento.Ativar();
                condicaoPagamento.PrepareSave();
                return await condicoesPagamentoDao.Inserir(condicaoPagamento);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<CondicoesPagamento> Editar(CondicoesPagamento condicaoPagamento)
        {
            string error = condicaoPagamento.Validation();
            if (error == null)
            {
                condicaoPagamento.PrepareSave();
                return await condicoesPagamentoDao.Editar(condicaoPagamento);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            CondicoesPagamento condicoesPagamento = new CondicoesPagamento();
            condicoesPagamento.codigo = codigo;
            condicoesPagamento.Inativar();
            condicoesPagamento.PrepareSave();
            return await condicoesPagamentoDao.Excluir(condicoesPagamento);
        }

        public async Task<IList<CondicoesPagamento>> Pesquisar(string str)
        {
            return await condicoesPagamentoDao.Pesquisar(str);
        }
    }
}
