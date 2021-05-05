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

        public async Task<Cotas> Inserir(Cotas cotas)
        {
            cotas.Ativar();
            cotas.PrepareSave();
            return await cotasDao.Inserir(cotas);
        }

        public async Task<Cotas> Editar(Cotas cotas)
        {
            cotas.PrepareSave();
            return await cotasDao.Editar(cotas);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Cotas cotas = new Cotas();
            cotas.codigo = codigo;
            cotas.Inativar();
            cotas.PrepareSave();
            return await cotasDao.Excluir(cotas);
        }

        public async Task<IList<Cotas>> Pesquisar(string str)
        {
            return await cotasDao.Pesquisar(str);
        }
    }
}
