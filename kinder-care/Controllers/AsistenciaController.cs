using kinder_care.Models;
using kinder_care.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace kinder_care.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly KinderCareContext _context;

        public AsistenciaController(KinderCareContext context)
        {
            _context = context;
        }

        //===========================[Relacion Ninos y Docentes]===========================
        public async Task<IActionResult> ListaNinos()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var userRole = User.FindFirstValue(ClaimTypes.Role);

            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);

            if (userRole == "Docente")
            {
                var estudiantes = await _context.RelDocenteNinoMateria
                    .Where(r => r.IdDocente == docente.IdDocente)
                    .Select(r => r.IdNino)
                    .Join(_context.Ninos,
                        rel => rel,
                        nino => nino.IdNino,
                        (rel, nino) => nino)
                    .ToListAsync();

            if (estudiantes.Any()) return View(estudiantes);

            return View();
            }
            ViewBag.Mensaje = "No tienes acceso a esta información.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Relacion()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var docente = await _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .FirstOrDefaultAsync(d => d.IdUsuario == userId);

            var estudiantesDisponibles = await _context.Ninos
                .Where(n => !_context.RelDocenteNinoMateria
                .Any(r => r.IdNino == n.IdNino))
                .ToListAsync();

            var vm = new RelacionDocenteNinosVM
            {
                IdDocente = docente.IdDocente,
                UsuarioNombre = docente.IdUsuarioNavigation.Nombre,
                UsuarioCedula = docente.IdUsuarioNavigation.Cedula,
                Ninos = estudiantesDisponibles
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Relacion(List<int> Ninos)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);

            if (Ninos == null || !Ninos.Any())
            {
                ViewBag.Mensaje = "Se debe escojer al menos un estudiante.";
                return RedirectToAction("Relacion");
            }

            foreach (var ninoId in Ninos)
            {
                if (!_context.RelDocenteNinoMateria.Any(r => r.IdDocente == docente.IdDocente && r.IdNino == ninoId))
                {
                    _context.RelDocenteNinoMateria.Add(new RelDocenteNinoMateria
                    {
                        IdDocente = docente.IdDocente,
                        IdNino = ninoId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ListaNinos");
        }


    }
}
