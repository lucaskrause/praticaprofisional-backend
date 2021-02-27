using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Entities;
using DAL.DataAccessObject;

namespace RUPsystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbstractController<T> : ControllerBase where T : AbstractEntity 
    {
        protected Service<T> _service;

        public AbstractController()
        {
            _service = new Service<T>(new DAO());
        }
    }
}
