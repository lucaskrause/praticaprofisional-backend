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

        public string validaCidade(Cidades cidade)
        {
            if (cidade.cidade == null || cidade.cidade == "")
            {
                return "Cidade obrigatória";
            }
            else if (cidade.ddd == null || cidade.ddd == "")
            {
                return "DDD obrigatório";
            }
            else if (cidade.codigoEstado <= 0)
            {
                return "Estado obrigatório";
            }
            else
            {
                return null;
            }
        }

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
            string error = validaCidade(cidade);
            if (error == null) {
            cidade.PrepareSave();
            cidade.Ativar();
            return await CidadesDao.Inserir(cidade);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Cidades> Editar(Cidades cidade)
        {
            string error = validaCidade(cidade);
            if (error == null)
            {
                cidade.PrepareSave();
                return await CidadesDao.Editar(cidade);
            }
            else
            {
                throw new Exception(error);
            }
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
