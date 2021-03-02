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
        private readonly PaisesService _service;
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
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<IActionResult> BuscarPorID(Paises pais)
        {
            try
            {
                Paises newPais = await _service.BuscarPorID(pais);
                return Ok(newPais);
            }
            catch (Exception)
            {

                throw;
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

        public override async Task<IActionResult> Editar(Paises pais)
        {
            try
            {
                pais = await _service.Editar(pais);
                return Ok(pais);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override async Task<IActionResult> Excluir(Paises pais)
        {
            try
            {
                bool result = await _service.Excluir(pais);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override async Task<IActionResult> Pesquisar(string str)
        {
            try
            {
                IList<Paises> listPaises = await _service.Pesquisar(str);
                return Ok(listPaises.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
