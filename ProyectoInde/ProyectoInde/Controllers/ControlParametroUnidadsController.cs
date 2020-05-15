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
    public class ControlParametroUnidadsController : Controller
    {
        private readonly bd_inde2Context _context;

        public ControlParametroUnidadsController(bd_inde2Context context)
        {
            _context = context;
        }

        // GET: ControlParametroUnidads
        public async Task<IActionResult> Index()
        {
            var bd_inde2Context = _context.ControlParametroUnidad.Include(c => c.CodHoraNavigation).Include(c => c.CodUnidadGeneradoraNavigation);
            return View(await bd_inde2Context.ToListAsync());
        }

        // GET: ControlParametroUnidads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlParametroUnidad = await _context.ControlParametroUnidad
                .Include(c => c.CodHoraNavigation)
                .Include(c => c.CodUnidadGeneradoraNavigation)
                .FirstOrDefaultAsync(m => m.CodControlParametroUnidad == id);
            if (controlParametroUnidad == null)
            {
                return NotFound();
            }

            return View(controlParametroUnidad);
        }

        // GET: ControlParametroUnidads/Create
        public IActionResult Create()
        {
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora");
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1");
            return View();
        }

        // POST: ControlParametroUnidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodControlParametroUnidad,DevanadosEstatorCDato1,DevanadosEstatorCDato2,DevanadosEstatorCDato3,DevanadosEstatorCDato4,DevanadosEstatorCDato5,DevanadosEstatorCDato6,DevanadosEstatorCDato7,DevanadosEstatorCDato8,CojineteEmpujeGeneradorCDato1,CojineteEmpujeGeneradorCDato2,CojineteGuiaGeneradorCDato1,CojineteGuiaGeneradorCDato2,AireSalienteEnfriadoresGeneradorCDato1,AireSalienteEnfriadoresGeneradorCDato2,AireSalienteEnfriadoresGeneradorCDato3,AireSalienteEnfriadoresGeneradorCDato4,AireSalienteEnfriadoresGeneradorCDato5,AireSalienteEnfriadoresGeneradorCDato6,EntradaSalidaAguaEnfriamientoCDato1,EntradaSalidaAguaEnfriamientoCDato2,AguaSalienteEnfriadorCojinetesGenC,AceiteCojineteGeneradorC,CojineteGuiaTurbinaC,CaudalAguaSelloLMx20E2639,PresionAguaSelloKgCm226422,TemperaturaSelloEje2276,CojineteEmpujeC,AireLlegadaEnfriadorCNo1,AireLlegadaEnfriadorCNo2,CojineteGuiaSuperiorC,AireSalidaEnfriadorCNo1,AireSalidaEnfriadorCNo2,PresionAguaSelloEje26432,PresionAguaSelloEje26431,PotenciaMw,FecIngreso,CodHora,CodUnidadGeneradora")] ControlParametroUnidad controlParametroUnidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controlParametroUnidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", controlParametroUnidad.CodHora);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlParametroUnidad.CodUnidadGeneradora);
            return View(controlParametroUnidad);
        }

        // GET: ControlParametroUnidads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlParametroUnidad = await _context.ControlParametroUnidad.FindAsync(id);
            if (controlParametroUnidad == null)
            {
                return NotFound();
            }
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", controlParametroUnidad.CodHora);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlParametroUnidad.CodUnidadGeneradora);
            return View(controlParametroUnidad);
        }

        // POST: ControlParametroUnidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodControlParametroUnidad,DevanadosEstatorCDato1,DevanadosEstatorCDato2,DevanadosEstatorCDato3,DevanadosEstatorCDato4,DevanadosEstatorCDato5,DevanadosEstatorCDato6,DevanadosEstatorCDato7,DevanadosEstatorCDato8,CojineteEmpujeGeneradorCDato1,CojineteEmpujeGeneradorCDato2,CojineteGuiaGeneradorCDato1,CojineteGuiaGeneradorCDato2,AireSalienteEnfriadoresGeneradorCDato1,AireSalienteEnfriadoresGeneradorCDato2,AireSalienteEnfriadoresGeneradorCDato3,AireSalienteEnfriadoresGeneradorCDato4,AireSalienteEnfriadoresGeneradorCDato5,AireSalienteEnfriadoresGeneradorCDato6,EntradaSalidaAguaEnfriamientoCDato1,EntradaSalidaAguaEnfriamientoCDato2,AguaSalienteEnfriadorCojinetesGenC,AceiteCojineteGeneradorC,CojineteGuiaTurbinaC,CaudalAguaSelloLMx20E2639,PresionAguaSelloKgCm226422,TemperaturaSelloEje2276,CojineteEmpujeC,AireLlegadaEnfriadorCNo1,AireLlegadaEnfriadorCNo2,CojineteGuiaSuperiorC,AireSalidaEnfriadorCNo1,AireSalidaEnfriadorCNo2,PresionAguaSelloEje26432,PresionAguaSelloEje26431,PotenciaMw,FecIngreso,CodHora,CodUnidadGeneradora")] ControlParametroUnidad controlParametroUnidad)
        {
            if (id != controlParametroUnidad.CodControlParametroUnidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controlParametroUnidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlParametroUnidadExists(controlParametroUnidad.CodControlParametroUnidad))
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
            ViewData["CodHora"] = new SelectList(_context.Hora1, "CodHora", "CodHora", controlParametroUnidad.CodHora);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlParametroUnidad.CodUnidadGeneradora);
            return View(controlParametroUnidad);
        }

        // GET: ControlParametroUnidads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlParametroUnidad = await _context.ControlParametroUnidad
                .Include(c => c.CodHoraNavigation)
                .Include(c => c.CodUnidadGeneradoraNavigation)
                .FirstOrDefaultAsync(m => m.CodControlParametroUnidad == id);
            if (controlParametroUnidad == null)
            {
                return NotFound();
            }

            return View(controlParametroUnidad);
        }

        // POST: ControlParametroUnidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controlParametroUnidad = await _context.ControlParametroUnidad.FindAsync(id);
            _context.ControlParametroUnidad.Remove(controlParametroUnidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControlParametroUnidadExists(int id)
        {
            return _context.ControlParametroUnidad.Any(e => e.CodControlParametroUnidad == id);
        }
    }
}
