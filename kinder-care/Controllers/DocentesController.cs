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

namespace kinder_care.Controllers
{
    public class DocentesController : Controller
    {
        private readonly KinderCareContext _context;
        private readonly PasswordHasher<Docentes> _passwordHasher;

        public DocentesController(KinderCareContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Docentes>();
        }

        //======================================================[VISTA INDEX]==========================================================================================
        public async Task<IActionResult> Index()
        {
            var kinderCareContext = _context.Docentes.Include(d => d.IdUsuarioNavigation);
            return View(await kinderCareContext.ToListAsync());
        }

        //======================================================[VISTA DETAILS]==========================================================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docentes = await _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdDocente == id);
            if (docentes == null)
            {
                return NotFound();
            }

            return View(docentes);
        }

        //======================================================[VISTA EDIT]==========================================================================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docentes = await _context.Docentes.FindAsync(id);
            if (docentes == null)
            {
                return NotFound();
            }

            ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(u => u.IdRol == 2).ToList(), "Id", "Nombre");
            return View(docentes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Docentes docentes, string? ActualizarContraseña)
        {
            if (docentes == null)
            {
                ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(u => u.IdRol == 2).ToList(), "Id", "Nombre");
                return View(docentes);
            }

            try
            {
                var docenteGuardado = await _context.Docentes
                    .Include(d => d.IdUsuarioNavigation) // Incluir la navegacion para poder llamar los datos de usuario desde docente
                    .FirstOrDefaultAsync(d => d.IdDocente == id);

                if (docenteGuardado == null)
                {
                    return NotFound();
                }

                docenteGuardado.FechaNacimiento = docentes.FechaNacimiento;
                docenteGuardado.GrupoAsignado = docentes.GrupoAsignado;
                docenteGuardado.Activo = docentes.Activo;

                if (docenteGuardado.IdUsuarioNavigation != null)
                {
                    docenteGuardado.IdUsuarioNavigation.Nombre = docentes.IdUsuarioNavigation.Nombre;
                    docenteGuardado.IdUsuarioNavigation.NumTelefono = docentes.IdUsuarioNavigation.NumTelefono;
                    docenteGuardado.IdUsuarioNavigation.Direccion = docentes.IdUsuarioNavigation.Direccion;
                    docenteGuardado.IdUsuarioNavigation.CorreoElectronico = docentes.IdUsuarioNavigation.CorreoElectronico;

                    // Actualizar la contraseña si se proporciona una nueva
                    if (!string.IsNullOrEmpty(ActualizarContraseña))
                    {
                        docenteGuardado.IdUsuarioNavigation.ContrasenaHash = _passwordHasher.HashPassword(docenteGuardado, ActualizarContraseña);
                    }

                    docenteGuardado.IdUsuarioNavigation.UltimaActualizacion = DateTime.Now; // Siempre actualiza la fecha
                }

                _context.Update(docenteGuardado);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocentesExists(docentes.IdDocente))
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

            var docentes = await _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdDocente == id);
            if (docentes == null)
            {
                return NotFound();
            }

            return View(docentes);
        }

        //======================================================[PROBAR QUE UN USUARIO EXISTE]==========================================================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docentes = await _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefaultAsync(d => d.IdDocente == id);

            if (docentes != null)
            {
                docentes.IdUsuarioNavigation.IdRol = 3; //Si se elimina un docente, el id del usuario relacionado se volvera un rol de padre
                _context.Docentes.Remove(docentes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocentesExists(int id)
        {
            return _context.Docentes.Any(e => e.IdDocente == id);
        }
    }
}
