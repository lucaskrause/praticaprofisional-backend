using System.Threading.Tasks;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;

namespace RUPsystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController<T> : ControllerBase where T : Pai
    {
        public readonly IService<T> _service;
    }
}
