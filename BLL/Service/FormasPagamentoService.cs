using DAL.DataAccessObject;
using RUPsystem.Entities;
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
            formaPagamento.PrepareSave();
            formaPagamento.Ativar();
            return await formasPagamentoDao.Inserir(formaPagamento);
        }

        public async Task<FormasPagamento> Editar(FormasPagamento formaPagamento)
        {
            formaPagamento.PrepareSave();
            return await formasPagamentoDao.Editar(formaPagamento);
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
