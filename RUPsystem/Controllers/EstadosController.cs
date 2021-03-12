using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class EstadosController : AbstractController<Estados>
    {
        private readonly new EstadosService _service;

        public EstadosController()
        {
            _service = new EstadosService();
        }

        [HttpGet]
        [Route("todos")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Estados> estados = await _service.ListarTodos();
                return Ok(estados.ToList());
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
                Estados newEstado = await _service.BuscarPorID(codigo);
                return Ok(newEstado);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(Estados estado)
        {
            try
            {
                Estados newEstado = await _service.Inserir(estado);
                return Ok(newEstado);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(Estados estado)
        {
            try
            {
                Estados newEstado = await _service.Editar(estado);
                return Ok(newEstado);
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
               bool result = await _service.Excluir(codigo);
                return Ok(result);
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
                IList<Estados> list = await _service.Pesquisar(str);
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
                throw;
            }
        }
    }
}
