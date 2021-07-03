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
    public class ContasBancariasController : AbstractController<ContasBancarias>
    {
        private readonly new ContasBancariasService _service;

        public ContasBancariasController()
        {
            _service = new ContasBancariasService();
        }

        [HttpGet]
        [Route("empresa/{codigo}")]
        public async Task<IActionResult> BuscarPorEmpresa(int codigo)
        {
            try
            {
                IList<ContasBancarias> contasBancarias = await _service.BuscarPorEmpresa(codigo);
                return Ok(contasBancarias.ToList());
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
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<ContasBancarias> list = await _service.ListarTodos();
                return Ok(list.ToList());
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
                ContasBancarias contaBancaria = await _service.BuscarPorID(codigo);
                return Ok(contaBancaria);
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
        public async Task<IActionResult> Inserir(ContasBancarias contaBancaria)
        {
            try
            {
                ContasBancarias newContaBancaria = await _service.Inserir(contaBancaria);
                return Ok(newContaBancaria);
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
        public async Task<IActionResult> Editar(ContasBancarias contaBancaria, int codigo)
        {
            try
            {
                contaBancaria.codigo = codigo;
                ContasBancarias newContaBancaria = await _service.Editar(contaBancaria);
                return Ok(newContaBancaria);
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
                IList<ContasBancarias> list = await _service.Pesquisar(str);
                return Ok(list.ToList());
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
