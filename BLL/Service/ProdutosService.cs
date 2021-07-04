using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ProdutosService : IService<Produtos>
    {
        private readonly ProdutosDAO produtosDao = null;

        public ProdutosService() => this.produtosDao = new ProdutosDAO();

        public async Task<IList<Produtos>> ListarTodos()
        {
            return await produtosDao.ListarTodos();
        }

        public async Task<Produtos> BuscarPorID(int id)
        {
            return await produtosDao.BuscarPorID(id);
        }

        public async Task<Produtos> Inserir(Produtos produto)
        {
            string error = produto.Validation();
            if (error == null)
            {
                produto.PrepareSave();
                produto.Ativar();
                return await produtosDao.Inserir(produto);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Produtos> Editar(Produtos produto)
        {
            string error = produto.Validation();
            if (error == null)
            {
                produto.PrepareSave();
                return await produtosDao.Editar(produto);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Produtos produto = new Produtos();
            produto.codigo = codigo;
            produto.PrepareSave();
            produto.Inativar();
            return await produtosDao.Excluir(produto);
        }

        public async Task<IList<Produtos>> Pesquisar(string str)
        {
            return await produtosDao.Pesquisar(str);
        }
    }
}
