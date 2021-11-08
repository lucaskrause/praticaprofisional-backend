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
    public class LocacoesController : AbstractController<Locacoes>
    {
        private readonly new LocacoesService _service;

        public LocacoesController()
        {
            _service = new LocacoesService();
        }

        [HttpPost]
        [Route("verificaArea")]
        public async Task<IActionResult> VerificaDisponibilidadeArea(LocacoesDTO locacao)
        {
            try
            {
                string result = await _service.VerificaDisponibilidadeArea(locacao);
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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Locacoes> list = await _service.ListarTodos();
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
                Locacoes locacao = await _service.BuscarPorID(codigo);
                return Ok(locacao);
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
        public async Task<IActionResult> Inserir(Locacoes locacao)
        {
            try
            {
                Locacoes newLocacao = await _service.Inserir(locacao);
                return Ok(newLocacao);
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
        public async Task<IActionResult> Editar(Locacoes locacao, int codigo)
        {
            try
            {
                locacao.codigo = codigo;
                Locacoes newLocacao = await _service.Editar(locacao);
                return Ok(newLocacao);
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
        [Route("pesquisar")]
        public async Task<IActionResult> Pesquisar(string str)
        {
            try
            {
                IList<Locacoes> list = await _service.Pesquisar(str);
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
