using BLL.DataTransferObjects;
using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class LocacoesService : IService<Locacoes>
    {
        private readonly LocacoesDAO locacoesDao = null;

        public LocacoesService() => this.locacoesDao = new LocacoesDAO();

        public async Task<string> VerificaDisponibilidadeArea(LocacoesDTO locacao)
        {
            return await locacoesDao.VerificaDisponibilidadeArea(locacao.dtLocacao, locacao.areasLocacao);
        }

        public async Task<IList<Locacoes>> ListarTodos()
        {
            return await locacoesDao.ListarTodos();
        }

        public async Task<Locacoes> BuscarPorID(int codigo)
        {
            return await locacoesDao.BuscarPorID(codigo);
        }

        public async Task<Locacoes> Inserir(Locacoes locacao)
        {
            string error = locacao.Validation();
            if (error == null) {
                locacao.PrepareSave();
                locacao.Ativar();
                return await locacoesDao.Inserir(locacao);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Locacoes> Editar(Locacoes locacao)
        {
            string error = locacao.Validation();
            if (error == null)
            {
                locacao.PrepareSave();
                return await locacoesDao.Editar(locacao);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Locacoes locacao = new Locacoes();
            locacao.codigo = codigo;
            locacao.PrepareSave();
            locacao.Inativar();
            return await locacoesDao.Excluir(locacao);
        }

        public async Task<IList<Locacoes>> Pesquisar(string str)
        {
            return await locacoesDao.Pesquisar(str);
        }
    }
}
