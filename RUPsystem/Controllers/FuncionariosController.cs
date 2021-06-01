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
                return UnprocessableEntity(ex.Message);
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
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> Inserir(FuncionariosDTO funcionario)
        {
            try
            {
                Funcionarios newFuncionario = await _service.Inserir(funcionario.ToFuncionario());
                return Ok(newFuncionario);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar/{codigo}")]
        public async Task<IActionResult> Editar(FuncionariosDTO funcionario, int codigo)
        {
            try
            {
                Funcionarios newFuncionario = await _service.Editar(funcionario.ToFuncionario(codigo));
                return Ok(newFuncionario);
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
                IList<Funcionarios> listFuncionarios = await _service.Pesquisar(str);
                return Ok(listFuncionarios.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
