using kinder_care.Models;
using kinder_care.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace kinder_care.Controllers;

[Authorize]
public class EventsController : Controller
{
    private readonly KinderCareContext _context;

    public EventsController(KinderCareContext context)
    {
        _context = context;
    }

    // GET: ManageEvents
    public async Task<IActionResult> ManageEvents(int pageNumber = 1)
    {
        int pageSize = 10; // Definir la cantidad de registros por página

        // Obtener el total de eventos
        var totalEventos = await _context.Actividades
            .Where(a => a.Activo == true)
            .CountAsync();

        // Obtener los eventos para la página actual
        var eventos = await _context.Actividades
            .Include(a => a.IdTipoActividadNavigation)
            .Where(a => a.Activo == true)
            .OrderByDescending(a => a.Fecha)
            .Skip((pageNumber - 1) * pageSize) // Salta los registros de las páginas anteriores
            .Take(pageSize) // Toma solo los 10 registros
            .ToListAsync();

        // Calcular el total de páginas
        int totalPages = (int)Math.Ceiling(totalEventos / (double)pageSize);

        // Pasar los datos a la vista
        ViewBag.CurrentPage = pageNumber;
        ViewBag.TotalPages = totalPages;
        ViewBag.TotalEventos = totalEventos;

        return View(eventos);
    }

    public async Task<IActionResult> Calendar()
    {
        var actividades = await _context.Actividades
            .Include(a => a.IdTipoActividadNavigation)
            .Where(a => a.Activo == true)
            .ToListAsync();

        List<object> eventos = new List<object>();

        foreach (var actividad in actividades)
        {
            eventos.Add(new
            {
                id = actividad.IdActividad,
                title = actividad.IdTipoActividadNavigation.NombreTipoActividad,
                start = actividad.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = actividad.Fecha.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss"),
                location = actividad.Lugar,
                description = actividad.Descripcion
            });
        }

        ViewBag.Eventos = JsonConvert.SerializeObject(eventos);
        return View();
    }

    // GET: CreateEvent
    public IActionResult CreateEvent()
    {
        ViewBag.TipoActividades = _context.TipoActividad
            .Where(t => t.Activo == true)
            .Select(t => new SelectListItem
            {
                Value = t.IdTipoActividad.ToString(),
                Text = t.NombreTipoActividad
            }).ToList();
        return View(new EventViewModel());
    }


    // POST: CreateEvent
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEvent(EventViewModel model)
    {
        if (ModelState.IsValid)
        {
            var nuevaActividad = new Actividades
            {
                Fecha = model.Fecha,
                Lugar = model.Lugar,
                Descripcion = model.Descripcion,
                IdTipoActividad = model.IdTipoActividad,
                Activo = true
            };

            _context.Actividades.Add(nuevaActividad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageEvents));
        }

        ViewBag.TipoActividades = await _context.TipoActividad
            .Where(t => t.Activo == true)
            .Select(t => new SelectListItem
            {
                Value = t.IdTipoActividad.ToString(),
                Text = t.NombreTipoActividad
            }).ToListAsync();
        return View(model);
    }


    // GET: EventDetails/{id}
    public async Task<IActionResult> EventDetails(int? id)
    {
        if (id == null)
            return NotFound();

        var actividad = await _context.Actividades
            .Include(a => a.IdTipoActividadNavigation)
            .FirstOrDefaultAsync(a => a.IdActividad == id);

        if (actividad == null)
            return NotFound();

        return View(actividad);
    }

    [HttpGet]
    public async Task<IActionResult> EditEvent(int id)
    {
        var actividad = await _context.Actividades
            .Include(a => a.IdTipoActividadNavigation)
            .FirstOrDefaultAsync(a => a.IdActividad == id);

        if (actividad == null)
        {
            TempData["ErrorMessage"] = "No se encontró el evento.";
            return RedirectToAction("ManageEvents");
        }

        var model = new EventViewModel
        {
            IdActividad = actividad.IdActividad,
            Fecha = actividad.Fecha,
            Lugar = actividad.Lugar,
            Descripcion = actividad.Descripcion,
            IdTipoActividad = actividad.IdTipoActividad
        };

        ViewBag.TipoActividades = _context.TipoActividad
            .Where(t => t.Activo == true)
            .Select(t => new SelectListItem
            {
                Value = t.IdTipoActividad.ToString(),
                Text = t.NombreTipoActividad
            }).ToList();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditEvent(EventViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Hay errores en el formulario. Revisa los datos.";
            return RedirectToAction("EventDetails", new { id = model.IdActividad });
        }

        try
        {
            var actividad = await _context.Actividades.FindAsync(model.IdActividad);
            if (actividad == null)
            {
                TempData["ErrorMessage"] = "El evento no existe.";
                return RedirectToAction("ManageEvents");
            }

            // Actualizamos los campos que pueden ser editados
            actividad.Fecha = model.Fecha;
            actividad.Lugar = model.Lugar;
            actividad.Descripcion = model.Descripcion;

            _context.Update(actividad);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Evento actualizado con éxito.";
            return RedirectToAction("ManageEvents");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error al actualizar el evento: {ex.Message}";
            return RedirectToAction("EventDetails", new { id = model.IdActividad });
        }
    }


    // GET: DeleteEvent/{id}
    public async Task<IActionResult> DeleteEvent(int? id)
    {
        if (id == null)
            return NotFound();

        var actividad = await _context.Actividades
            .Include(a => a.IdTipoActividadNavigation)
            .FirstOrDefaultAsync(a => a.IdActividad == id);

        if (actividad == null)
            return NotFound();

        return View(actividad);
    }

    [HttpPost, ActionName("DeleteEvent")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var actividad = await _context.Actividades.FindAsync(id);
        if (actividad != null)
        {
            actividad.Activo = false; // Desactiva el evento en lugar de eliminarlo si es necesario.
            await _context.SaveChangesAsync();
        }
        else
        {
            TempData["ErrorMessage"] = "El evento no pudo ser eliminado.";
        }

        return RedirectToAction(nameof(ManageEvents));
    }
}