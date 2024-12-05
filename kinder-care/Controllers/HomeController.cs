using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class HomeController(ILogger<HomeController> logger, KinderCareContext context) : Controller
{

    private readonly ILogger<HomeController> _logger = logger;
    private readonly KinderCareContext _context = context;

    public IActionResult Index()
    {
        var currentUserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
        int parsedCurrentUserId = int.TryParse(currentUserId, out int result) ? result : -1; // Convierte a entero o usa un valor no válido (-1) si falla
        var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        if (roles.Contains("Administrador"))
        {
            var usuariosInactivos = _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .Where(u => !u.Activo) 
                .ToList();

            if (usuariosInactivos == null || !usuariosInactivos.Any()) 
            {
                ViewData["Mensaje"] = "Actualmente no hay Usuarios Inactivos.";
            }
            else
            {
                ViewData["UsuariosInactivos"] = usuariosInactivos;
            }

            var ultimosPagos = _context.Pagos
                .Include(p => p.Padre) 
                .OrderByDescending(p => p.FechaPago) 
                .Take(5)
                .ToList();

            ViewData["UltimosPagos"] = ultimosPagos;

            ViewBag.RoleName = "Administrador";

        }
        else if (roles.Contains("Docente"))
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var docente = _context.Docentes.FirstOrDefault(d => d.IdUsuario == userId);

            var ultimasAusencias = _context.Asistencia
                .Include(a => a.IdNinoNavigation) 
                .Where(a => a.IdNinoNavigation.RelDocenteNinoMateria.Any(r => r.IdDocente == docente.IdDocente))
                .Where(a => !a.Presente) 
                .OrderByDescending(a => a.Fecha) 
                .Take(10) 
                .ToList();

            ViewData["UltimasAusencias"] = ultimasAusencias;

            ViewBag.RoleName = "Docente";
        }
        else if (roles.Contains("Padre")) 
        {

        }

        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}