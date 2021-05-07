using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class ContasBancariasController : AbstractController<ContasBancarias>
    {
        private readonly new ContasBancariasService _service;

        public ContasBancariasController()
        {
            _service = new ContasBancariasService();
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<ContasBancarias> list = await _service.ListarTodos();
                return Ok(list.ToList());
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
                ContasBancarias contaBancaria = await _service.BuscarPorID(codigo);
                return Ok(contaBancaria);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(ContasBancarias contaBancaria)
        {
            try
            {
                ContasBancarias newContaBancaria = await _service.Inserir(contaBancaria);
                return Ok(newContaBancaria);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(ContasBancarias contaBancaria)
        {
            try
            {
                ContasBancarias newContaBancaria = await _service.Editar(contaBancaria);
                return Ok(newContaBancaria);
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
                IList<ContasBancarias> list = await _service.Pesquisar(str);
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
