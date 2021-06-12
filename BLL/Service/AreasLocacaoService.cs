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
            area.PrepareSave();
            area.Ativar();
            return await areaLocacaoDao.Inserir(area);
        }

        public async Task<AreasLocacao> Editar(AreasLocacao area)
        {
            area.PrepareSave();
            return await areaLocacaoDao.Editar(area);
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
