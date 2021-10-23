using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ContasReceberService : IService<ContasReceber>
    {
        public Task<ContasReceber> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public Task<ContasReceber> Editar(ContasReceber entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(int codigo)
        {
            throw new NotImplementedException();
        }

        public Task<ContasReceber> Inserir(ContasReceber entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ContasReceber>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public Task<IList<ContasReceber>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
