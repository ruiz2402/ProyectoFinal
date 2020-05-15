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
    public class UsuariosxrolsController : Controller
    {
        private readonly bd_inde2Context _context;

        public UsuariosxrolsController(bd_inde2Context context)
        {
            _context = context;
        }

        // GET: Usuariosxrols
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Index()
        {
            var bd_inde2Context = _context.Usuariosxrol.Include(u => u.CodRolNavigation).Include(u => u.CodUsuarioNavigation);
            return View(await bd_inde2Context.ToListAsync());
        }

        // GET: Usuariosxrols/Details/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosxrol = await _context.Usuariosxrol
                .Include(u => u.CodRolNavigation)
                .Include(u => u.CodUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuarioXrol == id);
            if (usuariosxrol == null)
            {
                return NotFound();
            }

            return View(usuariosxrol);
        }

        // GET: Usuariosxrols/Create
        [Authorize(Roles = "administrador")]
        public IActionResult Create()
        {
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1");
            ViewData["CodUsuario"] = new SelectList(_context.Usuario, "CodUsuario", "Apellido");
            return View();
        }

        // POST: Usuariosxrols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Create([Bind("CodUsuarioXrol,CodUsuario,CodRol")] Usuariosxrol usuariosxrol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuariosxrol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1", usuariosxrol.CodRol);
            ViewData["CodUsuario"] = new SelectList(_context.Usuario, "CodUsuario", "Apellido", usuariosxrol.CodUsuario);
            return View(usuariosxrol);
        }

        // GET: Usuariosxrols/Edit/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosxrol = await _context.Usuariosxrol.FindAsync(id);
            if (usuariosxrol == null)
            {
                return NotFound();
            }
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1", usuariosxrol.CodRol);
            ViewData["CodUsuario"] = new SelectList(_context.Usuario, "CodUsuario", "Apellido", usuariosxrol.CodUsuario);
            return View(usuariosxrol);
        }

        // POST: Usuariosxrols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("CodUsuarioXrol,CodUsuario,CodRol")] Usuariosxrol usuariosxrol)
        {
            if (id != usuariosxrol.CodUsuarioXrol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuariosxrol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosxrolExists(usuariosxrol.CodUsuarioXrol))
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
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1", usuariosxrol.CodRol);
            ViewData["CodUsuario"] = new SelectList(_context.Usuario, "CodUsuario", "Apellido", usuariosxrol.CodUsuario);
            return View(usuariosxrol);
        }

        // GET: Usuariosxrols/Delete/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosxrol = await _context.Usuariosxrol
                .Include(u => u.CodRolNavigation)
                .Include(u => u.CodUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuarioXrol == id);
            if (usuariosxrol == null)
            {
                return NotFound();
            }

            return View(usuariosxrol);
        }

        // POST: Usuariosxrols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuariosxrol = await _context.Usuariosxrol.FindAsync(id);
            _context.Usuariosxrol.Remove(usuariosxrol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosxrolExists(int id)
        {
            return _context.Usuariosxrol.Any(e => e.CodUsuarioXrol == id);
        }
    }
}
