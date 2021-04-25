using DAL.DataAccessObject;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PessoasService : IService<Clientes>
    {
        private readonly PessoasDAO pessoasDao = null;

        public PessoasService() => this.pessoasDao = new PessoasDAO();

        public async Task<IList<Clientes>> ListarTodos()
        {
            return await pessoasDao.ListarTodos();
        }

        public async Task<Clientes> BuscarPorID(int codigo)
        {
            return await pessoasDao.BuscarPorID(codigo);
        }

        public async Task<Clientes> Inserir(Clientes pessoa)
        {
            pessoa.PrepareSave();
            pessoa.Ativar();
            return await pessoasDao.Inserir(pessoa);
        }

        public async Task<Clientes> Editar(Clientes pessoa)
        {
            pessoa.PrepareSave();
            return await pessoasDao.Editar(pessoa);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Clientes pessoa = new Clientes();
            pessoa.Codigo = codigo;
            pessoa.PrepareSave();
            pessoa.Inativar();
            return await pessoasDao.Excluir(pessoa);
        }

        public async Task<IList<Clientes>> Pesquisar(string str)
        {
            return await pessoasDao.Pesquisar(str);
        }
    }
}
