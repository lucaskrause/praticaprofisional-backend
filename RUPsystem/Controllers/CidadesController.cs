using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class CidadesController : AbstractController<Cidades>
    {
        private readonly new CidadesService _service;

        [HttpGet]
        [Route("todos")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Cidades> list = await _service.ListarTodos();
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
                Cidades cidade = await _service.BuscarPorID(codigo);
                return Ok(cidade);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(Cidades cidade)
        {
            try
            {
                Cidades newCidade = await _service.Inserir(cidade);
                return Ok(newCidade);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("editar")]
        public override async Task<IActionResult> Editar(Cidades cidade)
        {
            try
            {
                Cidades newCidade = await _service.Editar(cidade);
                return Ok(newCidade);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("excluir")]
        public override async Task<IActionResult> Excluir(Cidades cidade)
        {
            try
            {
                bool result = await _service.Excluir(cidade);
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
                return Ok();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
