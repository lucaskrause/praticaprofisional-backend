using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class EmpresasService : IService<Empresas>
    {
        private readonly EmpresasDAO empresasDao = null;

        public EmpresasService() => this.empresasDao = new EmpresasDAO();

        public async Task<IList<Empresas>> ListarTodos()
        {
            return await empresasDao.ListarTodos();
        }

        public async Task<Empresas> BuscarPorID(int codigo)
        {
            return await empresasDao.BuscarPorID(codigo);
        }

        public async Task<Empresas> Inserir(Empresas empresa)
        {
            empresa.Ativar();
            empresa.PrepareSave();
            return await empresasDao.Inserir(empresa);
        }

        public async Task<Empresas> Editar(Empresas empresa)
        {
            empresa.PrepareSave();
            return await empresasDao.Editar(empresa);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Empresas empresa = new Empresas();
            empresa.codigo = codigo;
            empresa.Inativar();
            empresa.PrepareSave();
            return await empresasDao.Excluir(empresa);
        }

        public async Task<IList<Empresas>> Pesquisar(string str)
        {
            return await empresasDao.Pesquisar(str);
        }
    }
}
