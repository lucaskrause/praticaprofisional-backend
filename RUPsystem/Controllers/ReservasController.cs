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
    public class ReservasController : AbstractController<Reservas>
    {
        private readonly new ReservasService _service;

        public ReservasController()
        {
            _service = new ReservasService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Reservas> list = await _service.ListarTodos();
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
                Reservas reserva = await _service.BuscarPorID(codigo);
                return Ok(reserva);
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
        public async Task<IActionResult> Inserir(Reservas reserva)
        {
            try
            {
                Reservas newReserva = await _service.Inserir(reserva);
                return Ok(newReserva);
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
        public async Task<IActionResult> Editar(Reservas reserva, int codigo)
        {
            try
            {
                reserva.codigo = codigo;
                Reservas newReserva = await _service.Editar(reserva);
                return Ok(newReserva);
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
                IList<Reservas> list = await _service.Pesquisar(str);
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
