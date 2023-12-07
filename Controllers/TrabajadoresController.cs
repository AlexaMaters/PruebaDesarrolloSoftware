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
    public class TrabajadoresController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public TrabajadoresController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index(string? sexo)
        {
            var trabajadoresPruebaContext = _context.Trabajadores.Include(t => t.IdDepartamentoNavigation).Include(t => t.IdDistritoNavigation).Include(t => t.IdProvinciaNavigation);
            if (!string.IsNullOrEmpty(sexo) && trabajadoresPruebaContext!=null)
            {
               var trabajadoresPruebaContextFil = _context.Trabajadores.Where(x => x.Sexo==sexo).Include(t => t.IdDepartamentoNavigation).Include(t => t.IdDistritoNavigation).Include(t => t.IdProvinciaNavigation);
                trabajadoresPruebaContext = trabajadoresPruebaContextFil;
            }
            return View(await trabajadoresPruebaContext.ToListAsync());
        }

        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trabajadore trabajadore)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(trabajadore);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, mensaje = "Operación exitosa" });
                }
                else
                {
                    // La validación del modelo falló, devuelve mensajes de error
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, errores = errors });
                }
            }
            catch (Exception ex)
            {
                // Maneja otros errores
                return Json(new { success = false, mensaje = "Error en el servidor", detalle = ex.Message });
            }
        }
        [HttpGet]
        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore == null)
            {
                return NotFound();
            }
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "IdDepartamento", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewBag.Distritos = new SelectList(_context.Distritos, "IdDistrito", "NombreDistrito", trabajadore.IdDistrito);
            ViewBag.Provincia = new SelectList(_context.Provincia, "IdProvincia", "NombreProvincia", trabajadore.IdProvincia);
            return Json(trabajadore);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Trabajadore trabajadore)
        {
            if (0 == trabajadore.Id)
            {
                return Json(new { success = false, errores = "El trabajador no existe." });
            }
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(trabajadore);
                        await _context.SaveChangesAsync();
                        return Json(new { success = true, mensaje = "Operación exitosa" });
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TrabajadoreExists(trabajadore.Id))
                        {
                            return Json(new { success = false, errores = "El trabajador no existe." });
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    // La validación del modelo falló, devuelve mensajes de error
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, errores = errors });
                }
            }
            catch (Exception ex)
            {
                // Maneja otros errores
                return Json(new { success = false, mensaje = "Error en el servidor", detalle = ex.Message });
            }
            
           
        }

        // POST: Trabajadores/Delete/5
        [HttpPut]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Trabajadores == null)
            {
                return Problem("Entity set 'TrabajadoresPruebaContext.Trabajadores'  is null.");
            }
           
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var trabajadore = await _context.Trabajadores.FindAsync(id);
                        if (trabajadore != null)
                        {
                            _context.Trabajadores.Remove(trabajadore);
                        }
                        await _context.SaveChangesAsync();
                        return Json(new { success = true, mensaje = "Operación exitosa" });
                    }
                    catch (Exception ex)
                    {
                       return Json(new { success = false, errores = ex });
                        
                    }
                }
                else
                {
                    // La validación del modelo falló, devuelve mensajes de error
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, errores = errors });
                }
            }
            catch (Exception ex)
            {
                // Maneja otros errores
                return Json(new { success = false, mensaje = "Error en el servidor", detalle = ex.Message });
            }
           
        }

        private bool TrabajadoreExists(int id)
        {
          return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
