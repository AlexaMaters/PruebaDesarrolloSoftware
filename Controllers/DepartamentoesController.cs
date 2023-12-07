using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaDesarrolloSoftware.Models;

namespace PruebaDesarrolloSoftware.Controllers
{
    public class DepartamentoesController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public DepartamentoesController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: Departamentoes
        [HttpGet]
        public IActionResult List()
        {
            var departamentos = _context.Departamentos.ToList();
            if (departamentos == null)
            {
                return NotFound();
            }

            return Json(departamentos);
        }
    }
}
