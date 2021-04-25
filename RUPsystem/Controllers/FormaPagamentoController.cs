using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using DAL.DataAccessObject;
using RUPsystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RUPsystem.Controllers
{
    public class FormaPagamentoController : AbstractController<FormasPagamento>
    {
        public override Task<IActionResult> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> Editar(FormasPagamento entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> Excluir(int codigo)
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> Inserir(FormasPagamento entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
