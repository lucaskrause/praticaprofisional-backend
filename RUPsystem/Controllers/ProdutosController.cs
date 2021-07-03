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
    public class ProdutosController : AbstractController<Produtos>
    {
        private readonly new ProdutosService _service;

        public ProdutosController()
        {
            _service = new ProdutosService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                IList<Produtos> listProdutos = await _service.ListarTodos();
                return Ok(listProdutos.ToList());
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
                Produtos newProduto = await _service.BuscarPorID(codigo);
                return Ok(newProduto);
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
        public async Task<IActionResult> Inserir(Produtos produto)
        {
            try
            {
                Produtos newProduto = await _service.Inserir(produto);
                return Created("/api/produtos/inserir", newProduto);
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
        public async Task<IActionResult> Editar(Produtos produto, int codigo)
        {
            try
            {
                produto.codigo = codigo;
                Produtos newProduto = await _service.Editar(produto);
                return Ok(newProduto);
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
                IList<Produtos> listProdutos = await _service.Pesquisar(str);
                return Ok(listProdutos.ToList());
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
