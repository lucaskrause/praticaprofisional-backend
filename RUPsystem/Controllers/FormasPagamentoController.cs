using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace RUPsystem.Controllers
{
    public class FormasPagamentoController : AbstractController<FormasPagamento>
    {
        public readonly new FormasPagamentoService _service;
        public FormasPagamentoController()
        {
            _service = new FormasPagamentoService();
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<FormasPagamento> ListaFormasPagamento = await _service.ListarTodos();
                return Ok(ListaFormasPagamento.ToList());
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpGet]
        [Route("{codigo}")]
        public override async Task<IActionResult> BuscarPorID(int codigo)
        {
            try
            {
                FormasPagamento formaPagamento = await _service.BuscarPorID(codigo);
                return Ok(formaPagamento);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(FormasPagamento formaPagamento)
        {
            try
            {
                FormasPagamento newFormaPagamento = await _service.Inserir(formaPagamento);
                return Ok(newFormaPagamento);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(FormasPagamento formaPagamento)
        {
            try
            {
                formaPagamento = await _service.Editar(formaPagamento);
                return Ok(formaPagamento);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
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
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPost]
        [Route("pesquisar")]
        public override async Task<IActionResult> Pesquisar(string str)
        {
            try
            {
                IList<FormasPagamento> listFormasPagamentos = await _service.Pesquisar(str);
                return Ok(listFormasPagamentos.ToList());
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
