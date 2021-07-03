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
                FormasPagamento formaPagamento = await _service.BuscarPorID(codigo);
                return Ok(formaPagamento);
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
        public async Task<IActionResult> Inserir(FormasPagamento formaPagamento)
        {
            try
            {
                FormasPagamento newFormaPagamento = await _service.Inserir(formaPagamento);
                return Ok(newFormaPagamento);
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
        public async Task<IActionResult> Editar(FormasPagamento formaPagamento, int codigo)
        {
            try
            {
                formaPagamento.codigo = codigo;
                FormasPagamento newFormaPagamento = await _service.Editar(formaPagamento);
                return Ok(newFormaPagamento);
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
                IList<FormasPagamento> listFormasPagamentos = await _service.Pesquisar(str);
                return Ok(listFormasPagamentos.ToList());
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
