using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class PrecificacoesDAO : DAO<Precificacoes>
    {
        public override Task<Precificacoes> BuscarPorID(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<Precificacoes> Editar(Precificacoes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(Precificacoes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Precificacoes> Inserir(Precificacoes entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Precificacoes>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<IList<Precificacoes>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
