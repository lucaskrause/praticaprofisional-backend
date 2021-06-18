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
    public class PrecificacoesController : AbstractController<Precificacoes>
    {
        private readonly new PrecificacoesService _service;

        public PrecificacoesController()
        {
            _service = new PrecificacoesService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Precificacoes> listPrecos = await _service.ListarTodos();
                return Ok(listPrecos.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpGet]
        [Route("{codigo}")]
        public async Task<IActionResult> BuscarPorID(int codigo)
        {
            try
            {
                Precificacoes preco = await _service.BuscarPorID(codigo);
                return Ok(preco);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> Inserir(PrecificacoesDTO preco)
        {
            try
            {
                Precificacoes newPreco = await _service.Inserir(preco.ToPreco());
                return Created("/api/precificacoes/inserir", newPreco);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar/{codigo}")]
        public async Task<IActionResult> Editar(PrecificacoesDTO preco, int codigo)
        {
            try
            {
                Precificacoes newPreco = await _service.Editar(preco.ToPreco(codigo));
                return Ok(newPreco);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
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
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("pesquisar")]
        public async Task<IActionResult> Pesquisar(string str)
        {
            try
            {
                IList<Precificacoes> listPrecos = await _service.Pesquisar(str);
                return Ok(listPrecos.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
