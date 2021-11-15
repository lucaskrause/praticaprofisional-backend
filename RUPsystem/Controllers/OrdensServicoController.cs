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
    public class OrdensServicoController : AbstractController<OrdensServico>
    {
        private readonly new OrdensServicoService _service;

        public OrdensServicoController()
        {
            _service = new OrdensServicoService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<OrdensServico> listOrdensServico = await _service.ListarTodos();
                return Ok(listOrdensServico.ToList());
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
                OrdensServico ordemServico = await _service.BuscarPorID(codigo);
                return Ok(ordemServico);
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
        public async Task<IActionResult> Inserir(OrdensServico ordemServico)
        {
            try
            {
                OrdensServico newOrdensServico = await _service.Inserir(ordemServico);
                return Created("/api/OrdensServico/inserir", newOrdensServico);
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

        [HttpPost]
        [Route("cancelar")]
        public async Task<IActionResult> Cancelar(OrdensServico ordemServico)
        {
            try
            {
                bool result = await _service.Cancelar(ordemServico);
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
                IList<OrdensServico> listOrdensServico = await _service.Pesquisar(str);
                return Ok(listOrdensServico.ToList());
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
