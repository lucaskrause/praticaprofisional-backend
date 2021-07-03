using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PrecificacoesService : IService<Precificacoes>
    {
        private readonly PrecificacoesDAO precificacoesDao = null;

        public PrecificacoesService() => this.precificacoesDao = new PrecificacoesDAO();

        public string validaPreco(Precificacoes preco)
        {
            if (preco.minPessoas <= 0)
            {
                return "Mínimo de Pessoas obrigatório";
            }
            else if (preco.maxPessoas <= 0)
            {
                return "Máximo de Pessoas obrigatório";
            }
            else if (preco.valor <= 0)
            {
                return "Valor obrigatório";
            }
            else
            {
                return null;
            }
        }

        public async Task<IList<Precificacoes>> ListarTodos()
        {
            return await precificacoesDao.ListarTodos();
        }

        public async Task<Precificacoes> BuscarPorID(int codigo)
        {
            return await precificacoesDao.BuscarPorID(codigo);
        }

        public async Task<Precificacoes> Inserir(Precificacoes preco)
        {
            string error = validaPreco(preco);
            if (error == null) {
                preco.PrepareSave();
                preco.Ativar();
                return await precificacoesDao.Inserir(preco);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Precificacoes> Editar(Precificacoes preco)
        {
            string error = validaPreco(preco);
            if (error == null)
            {
                preco.PrepareSave();
                return await precificacoesDao.Editar(preco);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Precificacoes preco = new Precificacoes();
            preco.codigo = codigo;
            preco.PrepareSave();
            preco.Inativar();
            return await precificacoesDao.Excluir(preco);
        }

         public async Task<IList<Precificacoes>> Pesquisar(string str)
        {
            return await precificacoesDao.Pesquisar(str);
        }
    }
}
