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
    public class DependentesController : AbstractController<Dependentes>
    {
        private readonly new DependentesService _service;

        public DependentesController()
        {
            _service = new DependentesService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
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
        public async Task<IActionResult> BuscarPorID(int codigo)
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
        public async Task<IActionResult> Inserir(DependentesDTO dependente)
        {
            try
            {
                Dependentes newDependente = await _service.Inserir(dependente.ToDependente());
                return Ok(newDependente);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar/{codigo}")]
        public async Task<IActionResult> Editar(DependentesDTO dependente, int codigo)
        {
            try
            {
                Dependentes newDependente = await _service.Editar(dependente.ToDependente(codigo));
                return Ok(newDependente);
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
