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
    public class ControlGeneracionDiariasController : Controller
    {
        private readonly bd_inde2Context _context;

        public ControlGeneracionDiariasController(bd_inde2Context context)
        {
            _context = context;
        }

        // GET: ControlGeneracionDiarias
        public async Task<IActionResult> Index()
        {
            var bd_inde2Context = _context.ControlGeneracionDiaria.Include(c => c.CodHoraNavigation).Include(c => c.CodUnidadGeneradoraNavigation);
            return View(await bd_inde2Context.ToListAsync());
        }

        // GET: ControlGeneracionDiarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGeneracionDiaria = await _context.ControlGeneracionDiaria
                .Include(c => c.CodHoraNavigation)
                .Include(c => c.CodUnidadGeneradoraNavigation)
                .FirstOrDefaultAsync(m => m.CodControlGeneracionDiaria == id);
            if (controlGeneracionDiaria == null)
            {
                return NotFound();
            }

            return View(controlGeneracionDiaria);
        }

        // GET: ControlGeneracionDiarias/Create
        public IActionResult Create()
        {
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora");
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1");
            return View();
        }

        // POST: ControlGeneracionDiarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodControlGeneracionDiaria,_230kVA,CampoV,CampoA,_138kvA,_138kvKv,PAparenteMw,PAparenteMvar,FecIngreso,CodHora,CodUnidadGeneradora")] ControlGeneracionDiaria controlGeneracionDiaria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controlGeneracionDiaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", controlGeneracionDiaria.CodHora);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlGeneracionDiaria.CodUnidadGeneradora);
            return View(controlGeneracionDiaria);
        }

        // GET: ControlGeneracionDiarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGeneracionDiaria = await _context.ControlGeneracionDiaria.FindAsync(id);
            if (controlGeneracionDiaria == null)
            {
                return NotFound();
            }
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", controlGeneracionDiaria.CodHora);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlGeneracionDiaria.CodUnidadGeneradora);
            return View(controlGeneracionDiaria);
        }

        // POST: ControlGeneracionDiarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodControlGeneracionDiaria,_230kVA,CampoV,CampoA,_138kvA,_138kvKv,PAparenteMw,PAparenteMvar,FecIngreso,CodHora,CodUnidadGeneradora")] ControlGeneracionDiaria controlGeneracionDiaria)
        {
            if (id != controlGeneracionDiaria.CodControlGeneracionDiaria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controlGeneracionDiaria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlGeneracionDiariaExists(controlGeneracionDiaria.CodControlGeneracionDiaria))
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
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", controlGeneracionDiaria.CodHora);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlGeneracionDiaria.CodUnidadGeneradora);
            return View(controlGeneracionDiaria);
        }

        // GET: ControlGeneracionDiarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGeneracionDiaria = await _context.ControlGeneracionDiaria
                .Include(c => c.CodHoraNavigation)
                .Include(c => c.CodUnidadGeneradoraNavigation)
                .FirstOrDefaultAsync(m => m.CodControlGeneracionDiaria == id);
            if (controlGeneracionDiaria == null)
            {
                return NotFound();
            }

            return View(controlGeneracionDiaria);
        }

        // POST: ControlGeneracionDiarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controlGeneracionDiaria = await _context.ControlGeneracionDiaria.FindAsync(id);
            _context.ControlGeneracionDiaria.Remove(controlGeneracionDiaria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControlGeneracionDiariaExists(int id)
        {
            return _context.ControlGeneracionDiaria.Any(e => e.CodControlGeneracionDiaria == id);
        }
    }
}
