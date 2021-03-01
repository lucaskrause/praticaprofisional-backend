using DAL.DataAccessObject;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PaisesService : IService<Paises>
    {
        private PaisesDAO paisesDao = null;

        public PaisesService() => this.paisesDao = new PaisesDAO();

        public Paises BuscarPorID(Paises entity)
        {
            throw new NotImplementedException();
        }

        public Paises Editar(Paises entity)
        {
            throw new NotImplementedException();
        }

        public bool Excluir(Paises entity)
        {
            throw new NotImplementedException();
        }

        public Paises Inserir(Paises pais)
        {
            if(pais.Pais != null && pais.Pais != "")
            {
                pais.PrepareSave();
                pais.Ativar();
                return paisesDao.Inserir(pais);
            } else
            {
                throw new Exception("Campo País precisa estar preenchido!");
            }
        }

        public IList<Paises> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public IList<Paises> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
