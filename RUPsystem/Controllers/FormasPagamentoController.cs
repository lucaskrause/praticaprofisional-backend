using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DataTransferObjects;
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
        public async Task<IActionResult> ListarTodos()
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
        public async Task<IActionResult> BuscarPorID(int codigo)
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
        public async Task<IActionResult> Inserir(FormasPagamentoDTO formaPagamento)
        {
            try
            {
                FormasPagamento newFormaPagamento = await _service.Inserir(formaPagamento.ToFormaPagamento());
                return Ok(newFormaPagamento);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPut]
        [Route("editar/{codigo}")]
        public async Task<IActionResult> Editar(FormasPagamentoDTO formaPagamento, int codigo)
        {
            try
            {
                FormasPagamento newFormaPagamento = await _service.Editar(formaPagamento.ToFormaPagamento(codigo));
                return Ok(newFormaPagamento);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
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
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPost]
        [Route("pesquisar")]
        public async Task<IActionResult> Pesquisar(string str)
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
