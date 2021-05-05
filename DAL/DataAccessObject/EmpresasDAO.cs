using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class EmpresasDAO : DAO<Empresas>
    {
        public override Task<IList<Empresas>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<Empresas> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public override Task<Empresas> Inserir(Empresas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Empresas> Editar(Empresas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(Empresas entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Empresas>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
