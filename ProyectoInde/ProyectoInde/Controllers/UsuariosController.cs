using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInde.Models;

namespace ProyectoInde.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly bd_inde2Context _context;

        public UsuariosController(bd_inde2Context context)
        {
            _context = context;
        }

        // GET: Usuarios
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            
            ViewData["CurrentSort"] = sortOrder;
            ViewData["Nombre"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["FecNacimiento"] = sortOrder == "FecNacimiento" ? "date_desc" : "FecNacimiento";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Usuario.Include(u => u.CodGeneroNavigation)
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Nombre.Contains(searchString) || s.Apellido.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Nombre);
                    break;
                case "FecNacimiento":
                    students = students.OrderBy(s => s.FecNacimiento);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.FecNacimiento);
                    break;
                default:
                    students = students.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Usuario>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));

            //var bd_inde2Context = _context.Usuario.Include(u => u.CodGeneroNavigation);
            //return View(await bd_inde2Context.ToListAsync());

           
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.CodGeneroNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "administrador")]
        public IActionResult Create()
        {
            ViewData["CodGenero"] = new SelectList(_context.Genero, "CodGenero", "Genero1");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Create([Bind("CodUsuario,Nombre,Apellido,Contrasenia,FecNacimiento,Email,CodGenero")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodGenero"] = new SelectList(_context.Genero, "CodGenero", "Genero1", usuario.CodGenero);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["CodGenero"] = new SelectList(_context.Genero, "CodGenero", "Genero1", usuario.CodGenero);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("CodUsuario,Nombre,Apellido,Contrasenia,FecNacimiento,Email,CodGenero")] Usuario usuario)
        {
            if (id != usuario.CodUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.CodUsuario))
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
            ViewData["CodGenero"] = new SelectList(_context.Genero, "CodGenero", "Genero1", usuario.CodGenero);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.CodGeneroNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.CodUsuario == id);
        }


        [HttpGet]
        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario login, string ReturnUrl = "")
        {

            using (bd_inde2Context dc = new bd_inde2Context())
            {

                ClaimsIdentity identity = null;
                bool isAuthenticated = false;
                // var ad = dc.TbUsuario.FirstOrDefault();

                var c = dc.Usuario.Where(w => w.Email == login.Email).FirstOrDefault();

                if (c != null)
                {
                    var d = dc.Usuario.Where(w => w.Contrasenia == login.Contrasenia).FirstOrDefault();

                    if (d != null)
                    {

                        var pers = dc.Usuariosxrol.Where(p => p.CodUsuario == c.CodUsuario).FirstOrDefault();

                        var rols = dc.Rol.Where(r => r.CodRol == pers.CodRol).FirstOrDefault();

                        identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, c.Email),
                        new Claim(ClaimTypes.Role, rols.Rol1)
                        }, CookieAuthenticationDefaults.AuthenticationScheme);
                        isAuthenticated = true;


                        if (isAuthenticated)
                        {
                            var principal = new ClaimsPrincipal(identity);
                            var loginA = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                            return RedirectToAction("Index", "Home");

                        }

                    }
                    else
                    {
                        RedirectToAction("Login");
                    }

                }
                else
                {
                    RedirectToAction("Login");

                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var loginA = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

        }

        //Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(string j)
        {
            var loginA = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
