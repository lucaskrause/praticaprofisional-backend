using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Entitys;

namespace RUPsystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : GenericController<Pais>
    {
        [HttpGet]
        [Route("teste")]
        public string test()
        {
            return "oi";
        }

        [HttpPost]
        [Route("create")]
        public IActionResult create(Pais pais)
        {
            return Created("", null);
        }
    }
}
