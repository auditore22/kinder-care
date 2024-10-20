using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kinder_care.Models;
using kinder_care.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace kinder_care.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly KinderCareContext _context;
        private readonly PasswordHasher<Usuarios> _passwordHasher;

        public UsuariosController(KinderCareContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuarios>();
        }

        //======================================================[VISTA INDEX]==========================================================================================
        public async Task<IActionResult> Index()
        {
            var kinderCareContext = _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .Select(u => new Usuarios
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    Cedula = u.Cedula,
                    CorreoElectronico = u.CorreoElectronico,
                    NumTelefono = u.NumTelefono,
                    Direccion = u.Direccion,
                    FechaCreacion = u.FechaCreacion,
                    UltimaActualizacion = u.UltimaActualizacion,
                    IdRol = u.IdRolNavigation.IdRol,
                    TokenRecovery = u.TokenRecovery ?? string.Empty // Usa un valor predeterminado si es null
                });

            return View(await kinderCareContext.ToListAsync());
        }

        //======================================================[VISTA DETAILS]==========================================================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        //======================================================[VISTA CREATE]==========================================================================================
        public IActionResult Create()
        {
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuarios usuarios)
        {

            usuarios.ContrasenaHash = _passwordHasher.HashPassword(usuarios, usuarios.ContrasenaHash);
            usuarios.TokenRecovery = usuarios.TokenRecovery ?? Guid.NewGuid().ToString(); // Generar un token si es null

            await _context.Usuarios.AddAsync(usuarios);
            await _context.SaveChangesAsync();

            if (usuarios.IdUsuario != 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Mensaje"] = "No se pudo crear el usuario";
                return View(usuarios);
            }
        }

        //======================================================[VISTA EDIT]==========================================================================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Nombre", usuarios.IdRol);
            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuarios usuarios)
        {
            if (usuarios == null)
            {
                ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Nombre");
                return View(usuarios);
            }

            try
            {
                var usuarioGuardado = await _context.Usuarios.FindAsync(id);
                if (usuarioGuardado == null)
                {
                    return NotFound();
                }

                usuarioGuardado.Nombre = usuarios.Nombre;
                usuarioGuardado.NumTelefono = usuarios.NumTelefono;
                usuarioGuardado.Direccion = usuarios.Direccion;
                usuarioGuardado.CorreoElectronico = usuarios.CorreoElectronico;
                usuarioGuardado.IdRol = usuarios.IdRol;
                usuarioGuardado.FechaCreacion = usuarioGuardado.FechaCreacion;
                usuarioGuardado.UltimaActualizacion = DateTime.Now;
                usuarioGuardado.Activo = usuarioGuardado.Activo;

                // Convertir la nueva contraseña en Hash
                if (!string.IsNullOrEmpty(usuarios.ContrasenaHash))
                {
                    usuarioGuardado.ContrasenaHash = _passwordHasher.HashPassword(usuarioGuardado, usuarios.ContrasenaHash);
                }

                _context.Update(usuarioGuardado);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(usuarios.IdUsuario))
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

        //======================================================[VISTA DELETE]==========================================================================================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Encuentra el usuario a eliminar
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios != null)
            {
                // Elimina los docentes relacionados
                var docentes = await _context.Docentes.Where(d => d.IdUsuario == id).ToListAsync(); //Esto es que busca usuarios relacionados a docentes
                if (docentes.Any())
                {
                    _context.Docentes.RemoveRange(docentes); //Esto es para eliminar los docentes para luego eliminar los usuarios
                }


                _context.Usuarios.Remove(usuarios); //Eliminamos el usuario
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        //======================================================[PROBAR QUE UN USUARIO EXISTE]==========================================================================================
        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
