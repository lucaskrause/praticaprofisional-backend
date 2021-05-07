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
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<CondicoesPagamento> listCondicoesPagamento = await _service.ListarTodos();
                return Ok(listCondicoesPagamento.ToList());
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
                CondicoesPagamento newCondicoesPagamento = await _service.BuscarPorID(codigo);
                return Ok(newCondicoesPagamento);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(CondicoesPagamento condicoesPagamento)
        {
            try
            {
                CondicoesPagamento newCondicoesPagamento = await _service.Inserir(condicoesPagamento);
                return Ok(newCondicoesPagamento);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(CondicoesPagamento condicoesPagamento)
        {
            try
            {
                condicoesPagamento = await _service.Editar(condicoesPagamento);
                return Ok(condicoesPagamento);
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
                IList<CondicoesPagamento> listCondicoesPagamento = await _service.Pesquisar(str);
                return Ok(listCondicoesPagamento.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
