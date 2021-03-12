using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using DAL.DataAccessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Entities;

namespace RUPsystem.Controllers
{
    public class PaisesController : AbstractController<Paises>
    {
        private readonly new PaisesService _service;
        
        public PaisesController()
        {
            _service = new PaisesService();
        }

        [HttpGet]
        [Route("todos")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Paises> listPaises = await _service.ListarTodos();
                return Ok(listPaises.ToList());
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
                Paises newPais = await _service.BuscarPorID(codigo);
                return Ok(newPais);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(Paises pais)
        {
            try
            {
                Paises newPais = await _service.Inserir(pais);
                return Ok(newPais);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(Paises pais)
        {
            try
            {
                pais = await _service.Editar(pais);
                return Ok(pais);
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
                IList<Paises> listPaises = await _service.Pesquisar(str);
                return Ok(listPaises.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
