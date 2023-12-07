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
    public class ProvinciumsController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public ProvinciumsController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: Provinciums
        public async Task<IActionResult> Index()
        {
            var trabajadoresPruebaContext = _context.Provincia.Include(p => p.IdDepartamentoNavigation);
            return View(await trabajadoresPruebaContext.ToListAsync());
        }

        // GET: Provinciums/Details/5
        [HttpGet]
        public IActionResult ListarPorDepartamento(int? idDepartamento)
        {
            if (idDepartamento == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincium =  _context.Provincia.Where(e => e.IdDepartamento == idDepartamento).ToList();
            if (provincium == null)
            {
                return NotFound();
            }

            return Json(provincium);
        }

    }
}
