using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using BLL.DataTransferObjects;

namespace RUPsystem.Controllers
{
    public class AreasLocacaoController : AbstractController<AreasLocacao>
    {
        private readonly new AreasLocacaoService _service;

        public AreasLocacaoController()
        {
            _service = new AreasLocacaoService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<AreasLocacao> listArea = await _service.ListarTodos();
                return Ok(listArea.ToList());
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
                AreasLocacao newArea = await _service.BuscarPorID(codigo);
                return Ok(newArea);
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
        public async Task<IActionResult> Inserir(AreasLocacao area)
        {
            try
            {
                AreasLocacao newArea = await _service.Inserir(area);
                return Created("/api/areasLocacao/inserir", newArea);
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
        public async Task<IActionResult> Editar(AreasLocacao area, int codigo)
        {
            try
            {
                area.codigo = codigo;
                AreasLocacao newArea = await _service.Editar(area);
                return Ok(newArea);
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
                IList<AreasLocacao> listAreas = await _service.Pesquisar(str);
                return Ok(listAreas.ToList());
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
