using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Entities;
using DAL.DataAccessObject;
using System.Data;

namespace RUPsystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController<T> : ControllerBase where T : AbstractEntity 
    {
        public IService<T> service;

        public abstract IActionResult Inserir(T entity);

        public abstract IActionResult Editar(T entity);

        public abstract IActionResult Excluir(T entity);

        public abstract IActionResult BuscarPorID(T entity);

        public abstract IActionResult ListarTodos();

        public abstract IActionResult Pesquisar(string str);
    }
}
