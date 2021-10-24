using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class ContasPagarController : AbstractController<ContasPagar>
    {
        private readonly new ContasPagarService _service;

        public ContasPagarController()
        {
            _service = new ContasPagarService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<ContasPagar> listContasPagar = await _service.ListarTodos();
                return Ok(listContasPagar.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new
                {
                    ex.Message,
                    Status = 422
                });
            }
        }

        [HttpPost]
        [Route("getParcela")]
        public async Task<IActionResult> BuscarParcela(ContasPagar contaPagar)
        {
            try
            {
                ContasPagar newContaPagar = await _service.BuscarParcela(contaPagar);
                return Ok(newContaPagar);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new
                {
                    ex.Message,
                    Status = 422
                });
            }
        }

        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> Inserir(ContasPagar contaPagar)
        {
            try
            {
                ContasPagar newContaPagar = await _service.Inserir(contaPagar);
                return Created("/api/contasPagar/inserir", newContaPagar);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new
                {
                    ex.Message,
                    Status = 422
                });
            }
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar(ContasPagar contaPagar)
        {
            try
            {
                ContasPagar newContaPagar = await _service.Editar(contaPagar);
                return Ok(newContaPagar);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new
                {
                    ex.Message,
                    Status = 422
                });
            }
        }

        [HttpDelete]
        [Route("excluir/{codigo}")]
        public async Task<IActionResult> Excluir(int codigo)
        {
            try
            {
                bool result = await _service.Excluir(codigo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new
                {
                    ex.Message,
                    Status = 422
                });
            }
        }

        [HttpPost]
        [Route("pesquisar")]
        public async Task<IActionResult> Pesquisar(string str)
        {
            try
            {
                IList<ContasPagar> listContasPagar = await _service.Pesquisar(str);
                return Ok(listContasPagar.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new
                {
                    ex.Message,
                    Status = 422
                });
            }
        }
    }
}
