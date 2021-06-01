using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class FornecedoresService
    {
        private readonly FornecedoresDAO fornecedorsDao = null;

        public FornecedoresService() => this.fornecedorsDao = new FornecedoresDAO();

        public async Task<IList<Fornecedores>> ListarTodos()
        {
            return await fornecedorsDao.ListarTodos();
        }

        public async Task<Fornecedores> BuscarPorID(int codigo)
        {
            return await fornecedorsDao.BuscarPorID(codigo);
        }

        public async Task<Fornecedores> Inserir(Fornecedores pessoa)
        {
            pessoa.PrepareSave();
            pessoa.Ativar();
            return await fornecedorsDao.Inserir(pessoa);
        }

        public async Task<Fornecedores> Editar(Fornecedores pessoa)
        {
            pessoa.PrepareSave();
            return await fornecedorsDao.Editar(pessoa);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Fornecedores fornecedor = new Fornecedores();
            fornecedor.codigo = codigo;
            fornecedor.PrepareSave();
            fornecedor.Inativar();
            return await fornecedorsDao.Excluir(fornecedor);
        }

        public async Task<IList<Fornecedores>> Pesquisar(string str)
        {
            return await fornecedorsDao.Pesquisar(str);
        }
    }
}
