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
    public class UsuarioxunidadGeneradorasController : Controller
    {
        private readonly bd_inde2Context _context;

        public UsuarioxunidadGeneradorasController(bd_inde2Context context)
        {
            _context = context;
        }

        // GET: UsuarioxunidadGeneradoras
        public async Task<IActionResult> Index()
        {
            var bd_inde2Context = _context.UsuarioxunidadGeneradora.Include(u => u.CodUnidadGeneradoraNavigation).Include(u => u.CodUsuarioNavigation);
            return View(await bd_inde2Context.ToListAsync());
        }

        // GET: UsuarioxunidadGeneradoras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioxunidadGeneradora = await _context.UsuarioxunidadGeneradora
                .Include(u => u.CodUnidadGeneradoraNavigation)
                .Include(u => u.CodUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuarioXunidadGeneradora == id);
            if (usuarioxunidadGeneradora == null)
            {
                return NotFound();
            }

            return View(usuarioxunidadGeneradora);
        }

        // GET: UsuarioxunidadGeneradoras/Create
        public IActionResult Create()
        {
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1");
            ViewData["CodUsuario"] = new SelectList(_context.Usuario, "CodUsuario", "Apellido");
            return View();
        }

        // POST: UsuarioxunidadGeneradoras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodUsuarioXunidadGeneradora,CodUsuario,CodUnidadGeneradora")] UsuarioxunidadGeneradora usuarioxunidadGeneradora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioxunidadGeneradora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", usuarioxunidadGeneradora.CodUnidadGeneradora);
            ViewData["CodUsuario"] = new SelectList(_context.Usuario, "CodUsuario", "Apellido", usuarioxunidadGeneradora.CodUsuario);
            return View(usuarioxunidadGeneradora);
        }

        // GET: UsuarioxunidadGeneradoras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioxunidadGeneradora = await _context.UsuarioxunidadGeneradora.FindAsync(id);
            if (usuarioxunidadGeneradora == null)
            {
                return NotFound();
            }
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", usuarioxunidadGeneradora.CodUnidadGeneradora);
            ViewData["CodUsuario"] = new SelectList(_context.Usuario, "CodUsuario", "Apellido", usuarioxunidadGeneradora.CodUsuario);
            return View(usuarioxunidadGeneradora);
        }

        // POST: UsuarioxunidadGeneradoras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodUsuarioXunidadGeneradora,CodUsuario,CodUnidadGeneradora")] UsuarioxunidadGeneradora usuarioxunidadGeneradora)
        {
            if (id != usuarioxunidadGeneradora.CodUsuarioXunidadGeneradora)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioxunidadGeneradora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioxunidadGeneradoraExists(usuarioxunidadGeneradora.CodUsuarioXunidadGeneradora))
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
            ViewData["CodUnidadGeneradora"] = new SelectList(_context.UnidadGeneradora, "CodUnidadGeneradora", "UnidadGeneradora1", usuarioxunidadGeneradora.CodUnidadGeneradora);
            ViewData["CodUsuario"] = new SelectList(_context.Usuario, "CodUsuario", "Apellido", usuarioxunidadGeneradora.CodUsuario);
            return View(usuarioxunidadGeneradora);
        }

        // GET: UsuarioxunidadGeneradoras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioxunidadGeneradora = await _context.UsuarioxunidadGeneradora
                .Include(u => u.CodUnidadGeneradoraNavigation)
                .Include(u => u.CodUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuarioXunidadGeneradora == id);
            if (usuarioxunidadGeneradora == null)
            {
                return NotFound();
            }

            return View(usuarioxunidadGeneradora);
        }

        // POST: UsuarioxunidadGeneradoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarioxunidadGeneradora = await _context.UsuarioxunidadGeneradora.FindAsync(id);
            _context.UsuarioxunidadGeneradora.Remove(usuarioxunidadGeneradora);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioxunidadGeneradoraExists(int id)
        {
            return _context.UsuarioxunidadGeneradora.Any(e => e.CodUsuarioXunidadGeneradora == id);
        }
    }
}
