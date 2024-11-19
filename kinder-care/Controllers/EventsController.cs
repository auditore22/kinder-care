using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class EventsController : Controller
{
    private readonly ILogger<EventsController> _logger;
    private readonly KinderCareContext _context;

    // Constructor para inicializar el logger y el contexto de la base de datos
    public EventsController(ILogger<EventsController> logger, KinderCareContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> ManageEvents()
    {
        ViewBag.CurrentSection = "ManageEvents";

        // Verificamos si _context está correctamente inicializado
        if (_context == null)
        {
            _logger.LogError("El contexto de la base de datos no está inicializado.");
            return RedirectToAction("Error");
        }

        try
        {
            var tiposActividad = await _context.TipoActividad
                .Where(t => t.Activo == true)
                .ToListAsync();

            ViewBag.TiposActividad = tiposActividad;

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener los tipos de actividad: {ex.Message}");
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(string title, int typeId, string date, string description)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(title) || typeId <= 0 || string.IsNullOrWhiteSpace(date))
            {
                TempData["ErrorMessage"] = "Datos inválidos. Verifica los campos ingresados.";
                return RedirectToAction("ManageEvents");
            }

            // Intentar convertir la fecha
            DateOnly fechaEvento;
            try
            {
                fechaEvento = DateOnly.Parse(date);
            }
            catch (FormatException)
            {
                TempData["ErrorMessage"] = "Formato de fecha inválido. Utiliza el formato yyyy-MM-dd.";
                return RedirectToAction("ManageEvents");
            }

            // Crear la nueva actividad
            var nuevaActividad = new Actividades
            {
                IdTipoActividad = typeId,
                Fecha = fechaEvento,
                Lugar = title,
                Descripcion = description,
                Activo = true
            };

            // Agregar la actividad a la base de datos
            _context.Actividades.Add(nuevaActividad);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Evento creado exitosamente.";
            return RedirectToAction("ManageEvents");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear el evento: {ex.Message}");
            TempData["ErrorMessage"] = "Error al crear el evento. Intenta de nuevo.";
            return RedirectToAction("ManageEvents");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var eventos = await _context.Actividades
            .Include(a => a.IdTipoActividadNavigation)
            .Where(a => a.Activo == true)
            .ToListAsync();

        var eventosJson = eventos.Select(e => new
        {
            id = e.IdActividad,
            title = e.Lugar,
            start = e.Fecha.ToString("yyyy-MM-dd"),
            description = e.Descripcion,
            tipoActividad = e.IdTipoActividadNavigation.NombreTipoActividad
        });

        return Json(eventosJson);
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
