using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class CotasController : AbstractController<Cotas>
    {
        private readonly new CotasService _service;

        public CotasController()
        {
            _service = new CotasService();
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Cotas> listCotas = await _service.ListarTodos();
                return Ok(listCotas.ToList());
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
                Cotas newCota = await _service.BuscarPorID(codigo);
                return Ok(newCota);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(Cotas cota)
        {
            try
            {
                Cotas newcota = await _service.Inserir(cota);
                return Ok(newcota);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(Cotas cota)
        {
            try
            {
                cota = await _service.Editar(cota);
                return Ok(cota);
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
                IList<Cotas> listCotas = await _service.Pesquisar(str);
                return Ok(listCotas.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
