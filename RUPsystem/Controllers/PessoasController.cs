using BLL.Service;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class PessoasController : AbstractController<Pessoas>
    {
        private readonly new PessoasService _service;

        public PessoasController()
        {
            _service = new PessoasService();
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Pessoas> list = await _service.ListarTodos();
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpGet]
        [Route("{codigo}")]
        public override async Task<IActionResult> BuscarPorID(int codigo)
        {
            try
            {
                IList<Pessoas> list = await _service.ListarTodos();
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(Pessoas pessoa)
        {
            try
            {
                IList<Pessoas> list = await _service.ListarTodos();
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(Pessoas pessoa)
        {
            try
            {
                IList<Pessoas> list = await _service.ListarTodos();
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpDelete]
        [Route("excluir/{codigo}")]
        public override async Task<IActionResult> Excluir(int codigo)
        {
            try
            {
                IList<Pessoas> list = await _service.ListarTodos();
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("pesquisar")]
        public override async Task<IActionResult> Pesquisar(string str)
        {
            try
            {
                IList<Pessoas> list = await _service.ListarTodos();
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
