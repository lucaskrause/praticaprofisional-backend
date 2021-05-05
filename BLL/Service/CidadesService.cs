using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CidadesService : IService<Cidades>
    {
        private readonly CidadesDAO CidadesDao = null;

        public CidadesService() => this.CidadesDao = new CidadesDAO();

        public async Task<IList<Cidades>> ListarTodos()
        {
            return await CidadesDao.ListarTodos();
        }

        public async Task<Cidades> BuscarPorID(int codigo)
        {
            return await CidadesDao.BuscarPorID(codigo);
        }

        public async Task<Cidades> Inserir(Cidades cidade)
        {
            cidade.PrepareSave();
            cidade.Ativar();
            return await CidadesDao.Inserir(cidade);
        }

        public async Task<Cidades> Editar(Cidades cidade)
        {
            cidade.PrepareSave();
            return await CidadesDao.Editar(cidade);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Cidades cidade = new Cidades();
            cidade.codigo = codigo;
            cidade.PrepareSave();
            cidade.Inativar();
            return await CidadesDao.Excluir(cidade);
        }

        public async Task<IList<Cidades>> Pesquisar(string str)
        {
            IList<Cidades> list = await CidadesDao.Pesquisar(str);
            return list;
        }
    }
}
