using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CotasService : IService<Cotas>
    {
        private readonly CotasDAO cotasDao = null;

        public CotasService() => this.cotasDao = new CotasDAO();

        public async Task<IList<Cotas>> ListarTodos()
        {
            return await cotasDao.ListarTodos();
        }
     
        public async Task<Cotas> BuscarPorID(int codigo)
        {
            return await cotasDao.BuscarPorID(codigo);
        }

        public async Task<Cotas> Inserir(Cotas cota)
        {
            cota.codigoEmpresa = 1;
            cota.Ativar();
            cota.PrepareSave();
            return await cotasDao.Inserir(cota);
        }

        public async Task<Cotas> Editar(Cotas cota)
        {
            cota.codigoEmpresa = 1;
            cota.PrepareSave();
            return await cotasDao.Editar(cota);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Cotas cota = new Cotas();
            cota.codigo = codigo;
            cota.Inativar();
            cota.PrepareSave();
            return await cotasDao.Excluir(cota);
        }

        public async Task<IList<Cotas>> Pesquisar(string str)
        {
            return await cotasDao.Pesquisar(str);
        }
    }
}
