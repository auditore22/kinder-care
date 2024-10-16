using System.Security.Claims;
using kinder_care.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

public class NinosController : Controller
{
    private readonly KinderCareContext _context;

    public NinosController(KinderCareContext context)
    {
        _context = context;
    }

    // GET: Ninos - trae a la vista los hijos del padre logueado
    public async Task<IActionResult> Index()
    {
        // Obtener el ID del padre logueado desde las Claims
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    
        // Verificar si el rol del usuario es 'Padre'
        var userRole = User.FindFirstValue(ClaimTypes.Role);
    
        if (userRole == "Padre")
        {
            // Consulta para obtener los niños relacionados con el padre logueado
            var ninos = await _context.RelPadresNinos
                .Where(r => r.IdPadre == userId)   // Filtrar por el ID del padre logueado
                .Select(r => r.IdNino)             // Seleccionar los IDs de los niños relacionados
                .Join(_context.Ninos,              // Unir con la tabla de niños
                    rel => rel,                  // Clave externa de la tabla rel_padres_ninos (IdNino)
                    nino => nino.IdNino,         // Clave primaria de la tabla ninos
                    (rel, nino) => nino)         // Seleccionar los datos de los niños
                .ToListAsync();

            if (ninos.Any())
            {
                return View(ninos); // Mostrar la lista de niños si hay registros
            }
            else
            {
                ViewBag.Mensaje = "No tienes niños asociados.";
                return View();
            }
        }
        else
        {
            ViewBag.Mensaje = "No tienes acceso a esta información.";
            return View();
        }
    }
}