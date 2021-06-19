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
    public class CategoriasController : AbstractController<Categorias>
    {
        private readonly new CategoriasService _service;

        public CategoriasController()
        {
            _service = new CategoriasService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Categorias> listCategorias = await _service.ListarTodos();
                return Ok(listCategorias.ToList());
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
                Categorias newCategoria = await _service.BuscarPorID(codigo);
                return Ok(newCategoria);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> Inserir(CategoriasDTO categoria)
        {
            try
            {
                Categorias newCategoria = await _service.Inserir(categoria.ToCategoria());
                return Created("/api/categorias/inserir", newCategoria);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("editar/{codigo}")]
        public async Task<IActionResult> Editar(CategoriasDTO categoria, int codigo)
        {
            try
            {
                Categorias newCategoria = await _service.Editar(categoria.ToCategoria(codigo));
                return Ok(newCategoria);
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
                IList<Categorias> listCategorias = await _service.Pesquisar(str);
                return Ok(listCategorias.ToList());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}