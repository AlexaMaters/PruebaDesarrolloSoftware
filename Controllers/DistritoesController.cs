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
    public class DistritoesController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public DistritoesController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }



        // GET: Distritoes/PorProvincia/5
        [HttpGet]
        public IActionResult ListPorProvincia(int? idProvincia)
        {
            if (idProvincia == null || _context.Distritos == null)
            {
                return NotFound();
            }

            var distritos =  _context.Distritos.Where(e=>e.IdProvincia==idProvincia).ToList();
            if (distritos == null)
            {
                return NotFound();
            }

            return Json(distritos);
        }

    }
}
