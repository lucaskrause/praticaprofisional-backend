using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DataTransferObjects;
using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace RUPsystem.Controllers
{
    public class ComprasController : AbstractController<Compras>
    {
        private readonly new ComprasService _service;

        public ComprasController()
        {
            _service = new ComprasService();
        }

        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> Find(Compras compra)
        {
            try
            {
                bool encontrou = await _service.Find(compra);
                return Ok(encontrou);
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
                IList<Compras> listCompras = await _service.ListarTodos();
                return Ok(listCompras.ToList());
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
        [Route("getCompra")]
        public async Task<IActionResult> BuscarCompra(Compras compra)
        {
            try
            {
                Compras newCompra = await _service.BuscarCompra(compra);
                return Ok(newCompra);
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
        public async Task<IActionResult> Inserir(Compras compra)
        {
            try
            {
                Compras newCompras = await _service.Inserir(compra);
                return Created("/api/compras/inserir", newCompras);
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
        public async Task<IActionResult> Editar(Compras pais, int codigo)
        {
            try
            {
                Compras newPais = await _service.Editar(pais);
                return Ok(newPais);
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
        [Route("parcelas")]
        public async Task<IActionResult> gerarParcelas(ParcelasDTO parcela)
        {
            try
            {
                var result = await _service.gerarParcelas(parcela);
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
        [Route("cancelar")]
        public async Task<IActionResult> Cancelar(Compras compra)
        {
            try
            {
                bool result = await _service.Cancelar(compra);
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
                IList<Compras> listCompras = await _service.Pesquisar(str);
                return Ok(listCompras.ToList());
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
