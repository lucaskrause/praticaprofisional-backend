using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class FuncionariosDAO : DAO<Funcionarios>
    {
        public override Task<IList<Funcionarios>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<Funcionarios> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public override Task<Funcionarios> Inserir(Funcionarios entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Funcionarios> Editar(Funcionarios entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(Funcionarios entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Funcionarios>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
