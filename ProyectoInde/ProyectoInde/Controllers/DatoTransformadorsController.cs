using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInde.Models;

namespace ProyectoInde.Controllers
{
    public class DatoTransformadorsController : Controller
    {
        private readonly bd_inde2Context _context;

        public DatoTransformadorsController(bd_inde2Context context)
        {
            _context = context;
        }

        // GET: DatoTransformadors
        public async Task<IActionResult> Index()
        {
            var bd_inde2Context = _context.DatoTransformador.Include(d => d.CodHoraNavigation).Include(d => d.CodTransformadorNavigation);
            return View(await bd_inde2Context.ToListAsync());
        }

        // GET: DatoTransformadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datoTransformador = await _context.DatoTransformador
                .Include(d => d.CodHoraNavigation)
                .Include(d => d.CodTransformadorNavigation)
                .FirstOrDefaultAsync(m => m.CodDatoTransformador == id);
            if (datoTransformador == null)
            {
                return NotFound();
            }

            return View(datoTransformador);
        }

        // GET: DatoTransformadors/Create
        public IActionResult Create()
        {
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora");
            ViewData["CodTransformador"] = new SelectList(_context.Transformador, "CodTransformador", "Transformador1");
            return View();
        }

        // POST: DatoTransformadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodDatoTransformador,PotenciaMw,TemperaturaAcC,TemperaturaDeC,NivelAc,VentIMA,FecIngreso,CodHora,CodTransformador")] DatoTransformador datoTransformador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datoTransformador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", datoTransformador.CodHora);
            ViewData["CodTransformador"] = new SelectList(_context.Transformador, "CodTransformador", "Transformador1", datoTransformador.CodTransformador);
            return View(datoTransformador);
        }

        // GET: DatoTransformadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datoTransformador = await _context.DatoTransformador.FindAsync(id);
            if (datoTransformador == null)
            {
                return NotFound();
            }
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", datoTransformador.CodHora);
            ViewData["CodTransformador"] = new SelectList(_context.Transformador, "CodTransformador", "Transformador1", datoTransformador.CodTransformador);
            return View(datoTransformador);
        }

        // POST: DatoTransformadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodDatoTransformador,PotenciaMw,TemperaturaAcC,TemperaturaDeC,NivelAc,VentIMA,FecIngreso,CodHora,CodTransformador")] DatoTransformador datoTransformador)
        {
            if (id != datoTransformador.CodDatoTransformador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datoTransformador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatoTransformadorExists(datoTransformador.CodDatoTransformador))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", datoTransformador.CodHora);
            ViewData["CodTransformador"] = new SelectList(_context.Transformador, "CodTransformador", "Transformador1", datoTransformador.CodTransformador);
            return View(datoTransformador);
        }

        // GET: DatoTransformadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datoTransformador = await _context.DatoTransformador
                .Include(d => d.CodHoraNavigation)
                .Include(d => d.CodTransformadorNavigation)
                .FirstOrDefaultAsync(m => m.CodDatoTransformador == id);
            if (datoTransformador == null)
            {
                return NotFound();
            }

            return View(datoTransformador);
        }

        // POST: DatoTransformadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datoTransformador = await _context.DatoTransformador.FindAsync(id);
            _context.DatoTransformador.Remove(datoTransformador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatoTransformadorExists(int id)
        {
            return _context.DatoTransformador.Any(e => e.CodDatoTransformador == id);
        }
    }
}
