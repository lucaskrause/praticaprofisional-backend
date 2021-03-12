using DAL.DataAccessObject;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PessoasService : IService<Pessoas>
    {
        private readonly PessoasDAO pessoasDao = null;

        public PessoasService() => this.pessoasDao = new PessoasDAO();

        public async Task<IList<Pessoas>> ListarTodos()
        {
            return await pessoasDao.ListarTodos();
        }

        public async Task<Pessoas> BuscarPorID(int codigo)
        {
            return await pessoasDao.BuscarPorID(codigo);
        }

        public async Task<Pessoas> Inserir(Pessoas pessoa)
        {
            pessoa.PrepareSave();
            pessoa.Ativar();
            return await pessoasDao.Inserir(pessoa);
        }

        public async Task<Pessoas> Editar(Pessoas pessoa)
        {
            pessoa.PrepareSave();
            return await pessoasDao.Editar(pessoa);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Pessoas pessoa = new Pessoas();
            pessoa.Codigo = codigo;
            pessoa.PrepareSave();
            pessoa.Inativar();
            return await pessoasDao.Excluir(pessoa);
        }

        public async Task<IList<Pessoas>> Pesquisar(string str)
        {
            return await pessoasDao.Pesquisar(str);
        }
    }
}
