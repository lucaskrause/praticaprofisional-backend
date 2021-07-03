using BLL.DataTransferObjects;
using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class CondicoesPagamentoController : AbstractController<CondicoesPagamento>
    {
        private readonly new CondicoesPagamentoService _service;
        public CondicoesPagamentoController()
        {
            _service = new CondicoesPagamentoService();
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<CondicoesPagamento> listCondicoesPagamento = await _service.ListarTodos();
                return Ok(listCondicoesPagamento.ToList());
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

        [HttpGet]
        [Route("{codigo}")]
        public async Task<IActionResult> BuscarPorID(int codigo)
        {
            try
            {
                CondicoesPagamento newCondicoesPagamento = await _service.BuscarPorID(codigo);
                return Ok(newCondicoesPagamento);
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
        public async Task<IActionResult> Inserir(CondicoesPagamento condicoesPagamento)
        {
            try
            {
                CondicoesPagamento newCondicoesPagamento = await _service.Inserir(condicoesPagamento);
                return Ok(newCondicoesPagamento);
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
        [Route("editar/{codigo}")]
        public async Task<IActionResult> Editar(CondicoesPagamento condicoesPagamento, int codigo)
        {
            try
            {
                condicoesPagamento.codigo = codigo;
                CondicoesPagamento newCondicoesPagamento = await _service.Editar(condicoesPagamento);
                return Ok(newCondicoesPagamento);
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
                IList<CondicoesPagamento> listCondicoesPagamento = await _service.Pesquisar(str);
                return Ok(listCondicoesPagamento.ToList());
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
