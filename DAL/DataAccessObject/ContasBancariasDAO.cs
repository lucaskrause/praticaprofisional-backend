using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ContasBancariasDAO : DAO<ContasBancarias>
    {
        public override Task<IList<ContasBancarias>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<ContasBancarias> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public override Task<ContasBancarias> Inserir(ContasBancarias entity)
        {
            throw new NotImplementedException();
        }

        public override Task<ContasBancarias> Editar(ContasBancarias entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(ContasBancarias entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<ContasBancarias>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
