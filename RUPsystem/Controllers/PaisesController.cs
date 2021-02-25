using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Entitys;

namespace RUPsystem.Controllers
{
    public class PaisesController : AbstractController<Paises>
    {
        public async Task<IActionResult> create(Paises pais)
        {
            try
            {
                var result = await _service.Save(pais);
                return Created("/api/paises/" + result.codigo, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Erro Interno do Servidor, tente novamente. Caso persista o erro, entrar em contato com time de desenvolvimento.",
                    Status = 500
                });
            }
        }
    }
}
