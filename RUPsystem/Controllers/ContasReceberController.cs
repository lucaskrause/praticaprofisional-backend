using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class ContasReceberController : AbstractController<ContasReceber>
    {
        private readonly new ContasReceberService _service;

        public ContasReceberController()
        {
            _service = new ContasReceberService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<ContasReceber> listContasReceber = await _service.ListarTodos();
                return Ok(listContasReceber.ToList());
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
        public async Task<IActionResult> BuscarParcela(ContasReceber contaReceber)
        {
            try
            {
                ContasReceber newContaReceber = await _service.BuscarParcela(contaReceber);
                return Ok(newContaReceber);
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
        public async Task<IActionResult> Inserir(ContasReceber contaReceber)
        {
            try
            {
                ContasReceber newContaReceber = await _service.Inserir(contaReceber);
                return Created("/api/contasReceber/inserir", newContaReceber);
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
        public async Task<IActionResult> Editar(ContasReceber contaReceber)
        {
            try
            {
                ContasReceber newContaReceber = await _service.Editar(contaReceber);
                return Ok(newContaReceber);
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
        [Route("receber")]
        public async Task<IActionResult> Receber(ContasReceber contaReceber)
        {
            try
            {
                ContasReceber newContaReceber = await _service.Receber(contaReceber);
                return Ok(newContaReceber);
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
                IList<ContasReceber> listContasReceber = await _service.Pesquisar(str);
                return Ok(listContasReceber.ToList());
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
