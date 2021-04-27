using DAL.DataAccessObject;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ClientesService : IService<Clientes>
    {
        private readonly ClientesDAO clientesDao = null;

        public ClientesService() => this.clientesDao = new ClientesDAO();

        public async Task<IList<Clientes>> ListarTodos()
        {
            return await clientesDao.ListarTodos();
        }

        public async Task<Clientes> BuscarPorID(int codigo)
        {
            return await clientesDao.BuscarPorID(codigo);
        }

        public async Task<Clientes> Inserir(Clientes pessoa)
        {
            pessoa.PrepareSave();
            pessoa.Ativar();
            return await clientesDao.Inserir(pessoa);
        }

        public async Task<Clientes> Editar(Clientes pessoa)
        {
            pessoa.PrepareSave();
            return await clientesDao.Editar(pessoa);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Clientes cliente = new Clientes();
            cliente.codigo = codigo;
            cliente.PrepareSave();
            cliente.Inativar();
            return await clientesDao.Excluir(cliente);
        }

        public async Task<IList<Clientes>> Pesquisar(string str)
        {
            return await clientesDao.Pesquisar(str);
        }
    }
}
