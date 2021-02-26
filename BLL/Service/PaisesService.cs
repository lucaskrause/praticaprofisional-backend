using DAL.DataAccessObject;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PaisesService : Service<Paises>
    {
        PaisesDAO paisesDao = null;

        public PaisesService() : base(new PaisesDAO())
        {
            this.paisesDao = (PaisesDAO)this.Dao;
        }
    }
}
