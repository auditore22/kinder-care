using System.Security.Claims;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers
{
    [Authorize(Roles = "Administrador, Docente")]
    public class TareaController : Controller
    {
        private readonly KinderCareContext _context;

        public TareaController(KinderCareContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Crear_Tarea(int idNino, int idProfesor, string nombre, string descripcion,
            DateTime fechaEntrega)
        {
            if (idProfesor <= 0 || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion) ||
                fechaEntrega == default)
            {
                return NotFound();
            }

            var tarea = new Tareas
            {
                IdProfesor = idProfesor,
                Nombre = nombre,
                Descripcion = descripcion,
                FechaAsignada = DateTime.Now,
                FechaEntrega = fechaEntrega,
                Activo = true
            };

            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarTareas @id_nino = {0}, @id_tarea = {1}, @id_profesor = {2}, @nombre = {3}, @descripcion = {4}, @calificacion = {5}, @fecha_asignada = {6}, @fecha_entrega = {7}, @activo = {8}, @accion = {9}",
                idNino, null!, tarea.IdProfesor, tarea.Nombre, tarea.Descripcion, tarea.Calificacion,
                tarea.FechaAsignada, tarea.FechaEntrega, tarea.Activo, "AGREGAR");

            if (result == 0)
            {
                return NotFound();
            }

            var rolUsuarioLogueado =
                User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

            if (rolUsuarioLogueado == "Administrador")
            {
                return RedirectToAction("Details_Admin", "Ninos", new { id = idNino });
            }
            else if (rolUsuarioLogueado == "Docente")
            {
                return RedirectToAction("Details_Docente", "Ninos", new { id = idNino, idDocente = tarea.IdProfesor });
            }

            return RedirectToAction("ListaNinos", "Asistencia");
        }

        [HttpGet]
        public async Task<IActionResult> Edit_Tarea(int idNino, int idTarea, int? idDocente)
        {
            if (idNino == 0 || idTarea == 0) return NotFound();

            // Obtener el niño y su información asociada
            var nino = await _context.Ninos
                .Include(n => n.ProgresoAcademico)
                .Include(n => n.ObservacionesDocentes)
                .Include(n => n.Asistencia)
                .Include(n => n.RelNinoAlergia).ThenInclude(ra => ra.Alergia)
                .Include(n => n.RelNinoMedicamento).ThenInclude(rm => rm.Medicamento)
                .Include(n => n.RelNinoCondicion).ThenInclude(rc => rc.Condicion)
                .Include(n => n.RelNinoContactoEmergencia).ThenInclude(re => re.ContactoEmergencia)
                .Include(n => n.RelNinoTarea).ThenInclude(rt => rt.Tareas)
                .FirstOrDefaultAsync(n => n.IdNino == idNino);

            if (nino == null) return NotFound();

            // Buscar la tarea específica para el niño
            var tarea = nino.RelNinoTarea?
                .FirstOrDefault(rt => rt.Tareas.IdTarea == idTarea)?.Tareas;

            if (tarea == null) return NotFound(); // Si no se encuentra la tarea

            // Obtener lista de docentes activos
            var listDocentes = await _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .Where(d => d.Activo)
                .Select(d => new
                {
                    d.IdDocente,
                    NombreDocente = d.IdUsuarioNavigation.Nombre
                })
                .ToListAsync();

            ViewBag.ListaDocentes = new SelectList(listDocentes, "IdDocente", "NombreDocente");

            // Obtener el usuario actual y validar el docente
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);

            if (docente != null)
            {
                // Si el usuario es docente, pasar el ID del docente al ViewBag
                ViewBag.RoleName = "Docente";
                ViewBag.DocenteId = docente.IdDocente; // Establecer el ID del docente en ViewBag
            }
            else
            {
                ViewBag.RoleName = "Administrador";
            }

            // Pasar la tarea y el niño al ViewBag o ViewModel
            ViewBag.Tarea = tarea;
            ViewBag.IdNino = nino.IdNino;

            return View(tarea); // Pasar la tarea como modelo para que los campos se llenen
        }


        [HttpPost]
        public async Task<IActionResult> Edit_Tarea(int idTarea, int idNino, int? idProfesor, string nombre,
            string descripcion, int? calificacion, DateTime fechaAsignada, DateTime fechaEntrega)
        {
            if (idTarea == 0) return NotFound();

            // Buscar la tarea existente por id
            var tarea = await _context.Tareas.FindAsync(idTarea);
            if (tarea == null) return NotFound();

            var nino = await _context.Ninos.FirstOrDefaultAsync(n => n.IdNino == idNino);
            if (nino == null)
            {
                return NotFound();
            }

            // Si el IdProfesor no se ha seleccionado (es null), asignamos el profesor actual de la tarea
            if (!idProfesor.HasValue)
            {
                idProfesor = tarea.IdProfesor; // Asigna el profesor actual
            }

            // Llamada al procedimiento almacenado para actualizar la tarea
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarTareas @id_nino = {0}, @id_tarea = {1}, @id_profesor = {2}, @nombre = {3}, @descripcion = {4}, @calificacion = {5}, @fecha_asignada = {6}, @fecha_entrega = {7}, @activo = {8}, @accion = {9}",
                idNino, idTarea, idProfesor, nombre, descripcion, calificacion!, fechaAsignada, fechaEntrega,
                true, "ACTUALIZAR");

            if (result == 0) return NotFound();

            await _context.SaveChangesAsync();

            // Obtener el rol del usuario logueado
            var rolUsuarioLogueado =
                User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

            // Redirigir según el rol del usuario
            if (rolUsuarioLogueado == "Administrador")
            {
                return RedirectToAction("Details_Admin", "Ninos", new { id = idNino });
            }
            else if (rolUsuarioLogueado == "Docente")
            {
                return RedirectToAction("Details_Docente", "Ninos", new { id = idNino });
            }

            return RedirectToAction("Details", "Ninos", new { id = idNino });
        }


        [HttpPost]
        public async Task<IActionResult> Eliminar_Tarea(int idTarea, int idNino)
        {
            if (idTarea <= 0) return NotFound();

            var buscarTarea = await _context.Tareas.FindAsync(idTarea);

            if (buscarTarea == null) return NotFound();

            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarTareas @id_nino = {0}, @id_tarea = {1}, @id_profesor = {2}, @nombre = {3}, @descripcion = {4}, @calificacion = {5}, @fecha_asignada = {6}, @fecha_entrega = {7}, @activo = {8}, @accion = {9}",
                null!, buscarTarea.IdTarea, null!, null!, null!, null!, null!, null!, null!, "ELIMINAR");

            if (result == 0) return NotFound();

            var rolUsuarioLogueado =
                User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

            if (rolUsuarioLogueado == "Administrador")
            {
                return RedirectToAction("Details_Admin", "Ninos", new { id = idNino });
            }
            else if (rolUsuarioLogueado == "Docente")
            {
                return RedirectToAction("Details_Docente", "Ninos", new { id = idNino });
            }

            return RedirectToAction("ListaNinos", "Asistencia");
        }
    }
}