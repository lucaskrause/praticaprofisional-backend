using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class AreasLocacaoService : IService<AreasLocacao>
    {
        private readonly AreasLocacaoDAO areaLocacaoDao = null;

        public AreasLocacaoService() => this.areaLocacaoDao = new AreasLocacaoDAO();

        public string validaAreaLocacao(AreasLocacao area)
        {
            if (area.descricao == null || area.descricao == "")
            {
                return "Área de Locação obrigatória";
            }
            else if (area.valor <= 0)
            {
                return "Valor obrigatório";
            }
            else
            {
                return null;
            }
        }

        public async Task<IList<AreasLocacao>> ListarTodos()
        {
            return await areaLocacaoDao.ListarTodos();
        }

        public async Task<AreasLocacao> BuscarPorID(int codigo)
        {
            return await areaLocacaoDao.BuscarPorID(codigo);
        }

        public async Task<AreasLocacao> Inserir(AreasLocacao area)
        {
            string error = validaAreaLocacao(area);
            if (error == null) {
                area.PrepareSave();
                area.Ativar();
                return await areaLocacaoDao.Inserir(area);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<AreasLocacao> Editar(AreasLocacao area)
        {
            string error = validaAreaLocacao(area);
            if (error == null)
            {
                area.PrepareSave();
                return await areaLocacaoDao.Editar(area);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            AreasLocacao area = new AreasLocacao();
            area.codigo = codigo;
            area.PrepareSave();
            area.Inativar();
            return await areaLocacaoDao.Excluir(area);
        }

        public async Task<IList<AreasLocacao>> Pesquisar(string str)
        {
            return await areaLocacaoDao.Pesquisar(str);
        }
    }
}
