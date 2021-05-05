using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class CondicoesPagamentoDAO : DAO<CondicoesPagamento>
    {
        public override Task<IList<CondicoesPagamento>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<CondicoesPagamento> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public override Task<CondicoesPagamento> Inserir(CondicoesPagamento entity)
        {
            throw new NotImplementedException();
        }

        public override Task<CondicoesPagamento> Editar(CondicoesPagamento entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(CondicoesPagamento entity)
        {
            throw new NotImplementedException();
        }
        
        public override Task<IList<CondicoesPagamento>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
