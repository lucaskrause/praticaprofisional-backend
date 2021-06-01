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
    public class EmpresasController : AbstractController<Empresas>
    {
        private readonly new EmpresasService _service;
        public EmpresasController()
        {
            _service = new EmpresasService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
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
        public async Task<IActionResult> BuscarPorID(int codigo)
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
        public async Task<IActionResult> Inserir(EmpresasDTO empresa)
        {
            try
            {
                Empresas newEmpresa = await _service.Inserir(empresa.ToEmpresa());
                return Ok(newEmpresa);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar/{codigo}")]
        public async Task<IActionResult> Editar(EmpresasDTO empresa, int codigo)
        {
            try
            {
                Empresas newEmpresa = await _service.Editar(empresa.ToEmpresa(codigo));
                return Ok(empresa);
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
