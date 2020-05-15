using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInde.Models;

namespace ProyectoInde.Controllers
{
    [Authorize(Roles = "administrador")]
    public class UnidadGeneradorasController : Controller
    {
        private readonly bd_inde2Context _context;

        public UnidadGeneradorasController(bd_inde2Context context)
        {
            _context = context;
        }

        // GET: UnidadGeneradoras
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadGeneradora.ToListAsync());
        }

        // GET: UnidadGeneradoras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadGeneradora = await _context.UnidadGeneradora
                .FirstOrDefaultAsync(m => m.CodUnidadGeneradora == id);
            if (unidadGeneradora == null)
            {
                return NotFound();
            }

            return View(unidadGeneradora);
        }

        // GET: UnidadGeneradoras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadGeneradoras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodUnidadGeneradora,UnidadGeneradora1")] UnidadGeneradora unidadGeneradora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidadGeneradora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidadGeneradora);
        }

        // GET: UnidadGeneradoras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadGeneradora = await _context.UnidadGeneradora.FindAsync(id);
            if (unidadGeneradora == null)
            {
                return NotFound();
            }
            return View(unidadGeneradora);
        }

        // POST: UnidadGeneradoras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodUnidadGeneradora,UnidadGeneradora1")] UnidadGeneradora unidadGeneradora)
        {
            if (id != unidadGeneradora.CodUnidadGeneradora)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadGeneradora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadGeneradoraExists(unidadGeneradora.CodUnidadGeneradora))
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
            return View(unidadGeneradora);
        }

        // GET: UnidadGeneradoras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadGeneradora = await _context.UnidadGeneradora
                .FirstOrDefaultAsync(m => m.CodUnidadGeneradora == id);
            if (unidadGeneradora == null)
            {
                return NotFound();
            }

            return View(unidadGeneradora);
        }

        // POST: UnidadGeneradoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadGeneradora = await _context.UnidadGeneradora.FindAsync(id);
            _context.UnidadGeneradora.Remove(unidadGeneradora);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadGeneradoraExists(int id)
        {
            return _context.UnidadGeneradora.Any(e => e.CodUnidadGeneradora == id);
        }
    }
}
