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

        [HttpPost]
        public IActionResult create(Paises pais)
        {
            try
            {
                _service.Inserir(pais);
                return Created("/api/paises/", pais);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro: ", ex);
            }
        }
    }
}
