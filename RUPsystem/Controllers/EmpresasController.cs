using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Controllers
{
    public class EmpresasController : AbstractController<Empresas>
    {
        private readonly new EmpresasService _service;
        public EmpresasController()
        {
            _service = new EmpresasService();
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Empresas> listEmpresas = await _service.ListarTodos();
                return Ok(listEmpresas.ToList());
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
                Empresas newEmpresa = await _service.BuscarPorID(codigo);
                return Ok(newEmpresa);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public override async Task<IActionResult> Inserir(Empresas empresa)
        {
            try
            {
                Empresas newEmpresa = await _service.Inserir(empresa);
                return Ok(newEmpresa);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        public override async Task<IActionResult> Editar(Empresas empresa)
        {
            try
            {
                empresa = await _service.Editar(empresa);
                return Ok(empresa);
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
                IList<Empresas> listEmpresas = await _service.Pesquisar(str);
                return Ok(listEmpresas.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
