using System.Threading.Tasks;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;

namespace RUPsystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController<T> : ControllerBase where T : AbstractEntity
    {
        public readonly IService<T> _service;

        public abstract Task<IActionResult> ListarTodos();

        public abstract Task<IActionResult> BuscarPorID(int codigo);

        public abstract Task<IActionResult> Inserir(T entity);

        public abstract Task<IActionResult> Editar(T entity);

        public abstract Task<IActionResult> Excluir(int codigo);

        public abstract Task<IActionResult> Pesquisar(string str);
    }
}
