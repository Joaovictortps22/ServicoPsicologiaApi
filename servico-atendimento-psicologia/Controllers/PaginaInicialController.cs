using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servico_atendimento_psicologia.Controllers
{
    public class PaginaInicialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
