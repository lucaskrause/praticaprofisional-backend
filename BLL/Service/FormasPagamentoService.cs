using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class FormasPagamentoService : IService<FormasPagamento>
    {
        private readonly FormasPagamentoDAO formasPagamentoDao = null;

        public FormasPagamentoService() => this.formasPagamentoDao = new FormasPagamentoDAO();

        public async Task<IList<FormasPagamento>> ListarTodos()
        {
            return await formasPagamentoDao.ListarTodos();
        }

        public async Task<FormasPagamento> BuscarPorID(int codigo)
        {
            return await formasPagamentoDao.BuscarPorID(codigo);
        }

        public async Task<FormasPagamento> Inserir(FormasPagamento formaPagamento)
        {
            string error = formaPagamento.Validation();
            if (error == null) {
                formaPagamento.PrepareSave();
                formaPagamento.Ativar();
                return await formasPagamentoDao.Inserir(formaPagamento);
            } 
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<FormasPagamento> Editar(FormasPagamento formaPagamento)
        {
            string error = formaPagamento.Validation();
            if (error == null)
            {
                formaPagamento.PrepareSave();
                return await formasPagamentoDao.Editar(formaPagamento);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            FormasPagamento formaPagamento = new FormasPagamento();
            formaPagamento.codigo = codigo;
            formaPagamento.PrepareSave();
            formaPagamento.Inativar();
            return await formasPagamentoDao.Excluir(formaPagamento);
        }

        public async Task<IList<FormasPagamento>> Pesquisar(string str)
        {
            return await formasPagamentoDao.Pesquisar(str);
        }
    }
}
