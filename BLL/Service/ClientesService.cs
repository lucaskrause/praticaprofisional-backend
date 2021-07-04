using DAL.DataAccessObject;
using DAL.Models;
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

        public async Task<Clientes> BuscarSocioPorID(int codigo)
        {
            return await clientesDao.BuscarSocioPorID(codigo);
        }

        public async Task<Clientes> Inserir(Clientes cliente)
        {
            string error = cliente.Validation();
            if (error == null)
            {
                cliente.PrepareSave();
                cliente.Ativar();
                return await clientesDao.Inserir(cliente);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<Clientes> Editar(Clientes cliente)
        {
            string error = cliente.Validation();
            if (error == null)
            {
                cliente.PrepareSave();
                return await clientesDao.Editar(cliente);
            }
            else
            {
                throw new Exception(error);
            }
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
