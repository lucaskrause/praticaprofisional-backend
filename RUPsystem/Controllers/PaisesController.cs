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
        new PaisesService _service;
        public PaisesController()
        {
            _service = new PaisesService();
        }

        public override IActionResult ListarTodos()
        {
            try
            {
                IList<Paises> listPaises = _service.ListarTodos();
                return Ok(listPaises.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IActionResult BuscarPorID(Paises pais)
        {
            try
            {
                Paises newPais = _service.BuscarPorID(pais);
                return Ok(newPais);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override IActionResult Inserir(Paises pais)
        {
            try
            {
                Paises newPais = _service.Inserir(pais);
                return Ok(newPais);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        public override IActionResult Editar(Paises pais)
        {
            try
            {
                pais = _service.Editar(pais);
                return Ok(pais);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IActionResult Excluir(Paises pais)
        {
            try
            {
                bool result = _service.Excluir(pais);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IActionResult Pesquisar(string str)
        {
            try
            {
                IList<Paises> listPaises = _service.Pesquisar(str);
                return Ok(listPaises.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
