using System.Diagnostics;
using System.Security.Claims;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class HomeController(ILogger<HomeController> logger, KinderCareContext context) : Controller
{
    private readonly KinderCareContext _context = context;
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        var currentUserId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value)
            .SingleOrDefault();
        var parsedCurrentUserId =
            int.TryParse(currentUserId, out var result)
                ? result
                : -1; // Convierte a entero o usa un valor no válido (-1) si falla
        var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        if (roles.Contains("Administrador"))
        {
            var usuariosInactivos = _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .Where(u => !u.Activo)
                .ToList();

            if (!usuariosInactivos.Any())
                ViewData["Mensaje"] = "Actualmente no hay Usuarios Inactivos.";
            else
                ViewData["UsuariosInactivos"] = usuariosInactivos;

            var ultimosPagos = _context.Pagos
                .Include(p => p.Padre)
                .OrderByDescending(p => p.FechaPago)
                .Take(5)
                .ToList();

            if (!ultimosPagos.Any())
                ViewData["Mensaje"] = "No hay pagos registrados.";
            else
                ViewData["UltimosPagos"] = ultimosPagos;

            var actividadesProximas = _context.Actividades
                .Include(a => a.RelNinoActividad)
                .ThenInclude(r => r.IdNinoNavigation)
                .Where(a => a.Fecha > DateTime.Now)
                .OrderBy(a => a.Fecha)
                .ToList();

            if (!actividadesProximas.Any())
                ViewData["Mensaje"] = "No hay Actividades.";
            else
                ViewData["ActividadesProximas"] = actividadesProximas;

            ViewBag.RoleName = "Administrador";
        }
        else if (roles.Contains("Docente"))
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var docente = _context.Docentes.FirstOrDefault(d => d.IdUsuario == userId);

            var ultimasAusencias = _context.Asistencia
                .Include(a => a.IdNinoNavigation)
                .Where(a => a.IdNinoNavigation.RelDocenteNinoMateria.Any(r => r.IdDocente == docente!.IdDocente))
                .Where(a => !a.Presente)
                .OrderByDescending(a => a.Fecha)
                .Take(10)
                .ToList();

            if (!ultimasAusencias.Any())
                ViewData["Mensaje"] = "Actualmente no hay Ausencias registradas.";
            else
                ViewData["UltimasAusencias"] = ultimasAusencias;

            var actividadesProximas = _context.Actividades
                .Include(a => a.RelNinoActividad)
                .ThenInclude(r => r.IdNinoNavigation)
                .Where(a => a.Fecha > DateTime.Now)
                .OrderBy(a => a.Fecha)
                .ToList();

            if (!actividadesProximas.Any())
                ViewData["Mensaje"] = "No hay Actividades.";
            else
                ViewData["ActividadesProximas"] = actividadesProximas;

            ViewBag.RoleName = "Docente";
        }
        else if (roles.Contains("Padre"))
        {
            var actividadesProximas = _context.Actividades
                .Include(a => a.RelNinoActividad)
                .ThenInclude(r => r.IdNinoNavigation)
                .Where(a => a.Fecha > DateTime.Now)
                .OrderBy(a => a.Fecha)
                .ToList();

            if (!actividadesProximas.Any())
                ViewData["Mensaje"] = "No hay Actividades.";
            else
                ViewData["ActividadesProximas"] = actividadesProximas;

            ViewBag.RoleName = "Padre";
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}