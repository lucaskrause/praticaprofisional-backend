using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RUPsystem.Context;
using RUPsystem.Entities;

namespace RUPsystem.Controllers
{
    public class AbstractController<T> : ControllerBase where T : AbstractEntity 
    {
        protected AbstractService<T> _service;

        public AbstractController()
        {
            _service = new AbstractService<T>();
        }
    }
}
