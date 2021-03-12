using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class PessoasControllers : AbstractController<Pessoas>
    {
        [HttpGet]
        [Route("Todos")]
        public override Task<IActionResult> ListarTodos()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{codigo}")]
        public override Task<IActionResult> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("inserir")]
        public override Task<IActionResult> Inserir(Pessoas pessoa)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("editar")]
        public override Task<IActionResult> Editar(Pessoas pessoa)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("excluir/{codigo}")]
        public override Task<IActionResult> Excluir(int codigo)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("pesquisar")]
        public override Task<IActionResult> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
