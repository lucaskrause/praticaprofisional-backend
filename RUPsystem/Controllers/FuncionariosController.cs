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
    public class FuncionariosController : AbstractController<Funcionarios>
    {
        private readonly new FuncionariosService _service;

        public FuncionariosController()
        {
            _service = new FuncionariosService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Funcionarios> listFuncionarios = await _service.ListarTodos();
                return Ok(listFuncionarios.ToList());
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
                Funcionarios newFuncionario = await _service.BuscarPorID(codigo);
                return Ok(newFuncionario);
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
        public async Task<IActionResult> Inserir(Funcionarios funcionario)
        {
            try
            {
                Funcionarios newFuncionario = await _service.Inserir(funcionario);
                return Ok(newFuncionario);
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
        public async Task<IActionResult> Editar(Funcionarios funcionario, int codigo)
        {
            try
            {
                funcionario.codigo = codigo;
                Funcionarios newFuncionario = await _service.Editar(funcionario);
                return Ok(newFuncionario);
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
                IList<Funcionarios> listFuncionarios = await _service.Pesquisar(str);
                return Ok(listFuncionarios.ToList());
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
