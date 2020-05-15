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
    public class ControlGeneralPtqUnidadsController : Controller
    {
        private readonly bd_inde2Context _context;

        public ControlGeneralPtqUnidadsController(bd_inde2Context context)
        {
            _context = context;
        }

        // GET: ControlGeneralPtqUnidads
        public async Task<IActionResult> Index()
        {
            var bd_inde2Context = _context.ControlGeneralPtqUnidad.Include(c => c.CodHora2Navigation).Include(c => c.CodUnidadGeneradoraNavigation);
            return View(await bd_inde2Context.ToListAsync());
        }

        // GET: ControlGeneralPtqUnidads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGeneralPtqUnidad = await _context.ControlGeneralPtqUnidad
                .Include(c => c.CodHora2Navigation)
                .Include(c => c.CodUnidadGeneradoraNavigation)
                .FirstOrDefaultAsync(m => m.CodControlGeneralPtqUnidad == id);
            if (controlGeneralPtqUnidad == null)
            {
                return NotFound();
            }

            return View(controlGeneralPtqUnidad);
        }

        // GET: ControlGeneralPtqUnidads/Create
        public IActionResult Create()
        {
            ViewData["CodHora2"] = new SelectList(_context.Hora2, "CodHora2", "CodHora2");
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1");
            return View();
        }

        // POST: ControlGeneralPtqUnidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodControlGeneralPtqUnidad,PresionAceiteRegulador11261,PresionAceiteControlPilotoReg11262,PresionAceiteRefrigeradorReg11263,TempAceiteReguladorC1180,TempCojineteGuiaTurbinaC2722,PAeEntradaEnfriadorCGBar26434,QAeCGLMinX20E2727,PAeSalidaEnfriadorCGBar26435,TAeSalidaEnfriadorCG27362,TAceiteCGSalidaEnfriadorC27331,TAceiteCGEntradaEnfriadorC27331,FlujoAceiteCGTurbinaLMinx5E2727,FlujoAeCCombinado,FlujoAeGenerador,PAguasAbajoVeBar654,PAguasArribaVeBar853,PAguasArribaVeBar651,PAceiteMandoVeBar652,IndNivelDesfogueNormalAnormal,PBombaAeBar1894,PAeGeneradorBar2740,TAeTurbinaC27361,PAeTurbinaBar26431,PAlKgCm226432,FecIngreso,CodHora2,CodUnidadGeneradora")] ControlGeneralPtqUnidad controlGeneralPtqUnidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controlGeneralPtqUnidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodHora2"] = new SelectList(_context.Hora2, "CodHora2", "CodHora2", controlGeneralPtqUnidad.CodHora2);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlGeneralPtqUnidad.CodUnidadGeneradora);
            return View(controlGeneralPtqUnidad);
        }

        // GET: ControlGeneralPtqUnidads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGeneralPtqUnidad = await _context.ControlGeneralPtqUnidad.FindAsync(id);
            if (controlGeneralPtqUnidad == null)
            {
                return NotFound();
            }
            ViewData["CodHora2"] = new SelectList(_context.Hora2, "CodHora2", "CodHora2", controlGeneralPtqUnidad.CodHora2);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlGeneralPtqUnidad.CodUnidadGeneradora);
            return View(controlGeneralPtqUnidad);
        }

        // POST: ControlGeneralPtqUnidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodControlGeneralPtqUnidad,PresionAceiteRegulador11261,PresionAceiteControlPilotoReg11262,PresionAceiteRefrigeradorReg11263,TempAceiteReguladorC1180,TempCojineteGuiaTurbinaC2722,PAeEntradaEnfriadorCGBar26434,QAeCGLMinX20E2727,PAeSalidaEnfriadorCGBar26435,TAeSalidaEnfriadorCG27362,TAceiteCGSalidaEnfriadorC27331,TAceiteCGEntradaEnfriadorC27331,FlujoAceiteCGTurbinaLMinx5E2727,FlujoAeCCombinado,FlujoAeGenerador,PAguasAbajoVeBar654,PAguasArribaVeBar853,PAguasArribaVeBar651,PAceiteMandoVeBar652,IndNivelDesfogueNormalAnormal,PBombaAeBar1894,PAeGeneradorBar2740,TAeTurbinaC27361,PAeTurbinaBar26431,PAlKgCm226432,FecIngreso,CodHora2,CodUnidadGeneradora")] ControlGeneralPtqUnidad controlGeneralPtqUnidad)
        {
            if (id != controlGeneralPtqUnidad.CodControlGeneralPtqUnidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controlGeneralPtqUnidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlGeneralPtqUnidadExists(controlGeneralPtqUnidad.CodControlGeneralPtqUnidad))
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
            ViewData["CodHora2"] = new SelectList(_context.Hora2, "CodHora2", "CodHora2", controlGeneralPtqUnidad.CodHora2);
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", controlGeneralPtqUnidad.CodUnidadGeneradora);
            return View(controlGeneralPtqUnidad);
        }

        // GET: ControlGeneralPtqUnidads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGeneralPtqUnidad = await _context.ControlGeneralPtqUnidad
                .Include(c => c.CodHora2Navigation)
                .Include(c => c.CodUnidadGeneradoraNavigation)
                .FirstOrDefaultAsync(m => m.CodControlGeneralPtqUnidad == id);
            if (controlGeneralPtqUnidad == null)
            {
                return NotFound();
            }

            return View(controlGeneralPtqUnidad);
        }

        // POST: ControlGeneralPtqUnidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controlGeneralPtqUnidad = await _context.ControlGeneralPtqUnidad.FindAsync(id);
            _context.ControlGeneralPtqUnidad.Remove(controlGeneralPtqUnidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControlGeneralPtqUnidadExists(int id)
        {
            return _context.ControlGeneralPtqUnidad.Any(e => e.CodControlGeneralPtqUnidad == id);
        }
    }
}
