using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class DependentesController : AbstractController<Dependentes>
    {
        private readonly new DependentesService _service;

        public DependentesController()
        {
            _service = new DependentesService();
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Dependentes> listDependentes = await _service.ListarTodos();
                return Ok(listDependentes.ToList());
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
                Dependentes newDependente = await _service.BuscarPorID(codigo);
                return Ok(newDependente);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(Dependentes dependente)
        {
            try
            {
                Dependentes newDependente = await _service.Inserir(dependente);
                return Ok(newDependente);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(Dependentes dependente)
        {
            try
            {
                dependente = await _service.Editar(dependente);
                return Ok(dependente);
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
                IList<Dependentes> listDependentes = await _service.Pesquisar(str);
                return Ok(listDependentes.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
