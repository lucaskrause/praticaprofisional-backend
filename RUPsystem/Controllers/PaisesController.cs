using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Entities;

namespace RUPsystem.Controllers
{
    public class PaisesController : AbstractController<Paises>
    {
        new PaisesService _service;
        public PaisesController()
        {
            _service = new PaisesService();
        }

        public virtual void Inserir(Object obj)
        {
            Dao.Inserir(obj);
        }

        public virtual void Editar(Object obj)
        {
            Dao.Editar(obj);
        }

        public virtual void Excluir(Object obj)
        {
            Dao.Excluir(obj);
        }

        public virtual object BuscarPorID(Object obj)
        {
            return this.Dao.BuscarPorID(obj);
        }

        public virtual DataTable ListarTodos()
        {
            return Dao.ListarTodos();
        }

        public virtual object Pesquisar(string obj)
        {
            return Dao.Pesquisar(obj);
        }
    }
}
