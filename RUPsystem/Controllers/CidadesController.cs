using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DataTransferObjects;

namespace RUPsystem.Controllers
{
    public class CidadesController : AbstractController<Cidades>
    {
        private readonly new CidadesService _service;

        public CidadesController()
        {
            _service = new CidadesService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Cidades> list = await _service.ListarTodos();
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
                Cidades cidade = await _service.BuscarPorID(codigo);
                return Ok(cidade);
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
        public async Task<IActionResult> Inserir(Cidades cidade)
        {
            try
            {
                Cidades newCidade = await _service.Inserir(cidade);
                return Ok(newCidade);
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
        public async Task<IActionResult> Editar(Cidades cidade, int codigo)
        {
            try
            {
                cidade.codigo = codigo;
                Cidades newCidade = await _service.Editar(cidade);
                return Ok(newCidade);
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
                IList<Cidades> list = await _service.Pesquisar(str);
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
