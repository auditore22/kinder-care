using System.Data;
using System.Security.Claims;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class TareaController : Controller
{
    private readonly KinderCareContext _context;

    public TareaController(KinderCareContext context)
    {
        _context = context;
    }

    /*[HttpPost]
    public async Task<IActionResult> Crear_Tarea(
        int IdNino,
        int IdProfesor,
        string NombreTarea,
        string Descripcion,
        DateTime FechaEntrega,
        IFormFile? DocTareaDocente,
        int? Extencion,
        bool Activo)
    {
        if (IdNino == 0 || IdProfesor == 0)
        {
            return NotFound();
        }

        var rolUsuarioLogueado = User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .SingleOrDefault();

        byte[]? msBit = null;

        if (DocTareaDocente != null && Extencion != null)
        {
            using (var ms = new MemoryStream())
            {
                await DocTareaDocente.CopyToAsync(ms);

                // Validar el tamaño del archivo (5 MB máximo)
                if (DocTareaDocente.Length > 5242880) // 5 MB
                {
                    TempData["ErrorMessage"] = "El archivo supera el tamaño máximo permitido de 5 MB.";

                    // Redirigir según el rol del usuario
                    if (rolUsuarioLogueado == "Administrador")
                        return RedirectToAction("Details_Admin", "Ninos", new { id = IdNino });

                    if (rolUsuarioLogueado == "Docente")
                        return RedirectToAction("Details_Docente", "Ninos",
                            new { id = IdNino, idDocente = IdProfesor });
                }

                msBit = ms.ToArray();
            }
        }

        Tareas Task = new Tareas()
        {
            IdProfesor = IdProfesor,
            Nombre = NombreTarea,
            Descripcion = Descripcion,
            FechaAsignada = DateTime.Now,
            FechaEntrega = FechaEntrega,
            Activo = Activo,
            DocTareaDocente = msBit,
            Extencion = Extencion
        };

        try
        {
            // Definir los parámetros explícitamente
            var parameters = new[]
            {
                new SqlParameter("@id_nino", IdNino),
                new SqlParameter("@id_tarea", DBNull.Value), // Null porque es una inserción
                new SqlParameter("@id_profesor", Task.IdProfesor),
                new SqlParameter("@nombre", Task.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@descripcion", Task.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@calificacion", DBNull.Value), // Null porque no se especifica calificación al crear
                new SqlParameter("@fecha_asignada", Task.FechaAsignada),
                new SqlParameter("@fecha_entrega", Task.FechaEntrega),
                new SqlParameter("@activo", Task.Activo),
                new SqlParameter("@extencion", Task.Extencion ?? (object)DBNull.Value),
                new SqlParameter("@doc_tarea_docente", Task.DocTareaDocente ?? (object)DBNull.Value),
                new SqlParameter("@doc_tarea_nino", DBNull.Value), // Null porque no hay archivo del niño en este caso
                new SqlParameter("@accion", "AGREGAR")
            };

            // Ejecutar el procedimiento almacenado
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarTareas @id_nino, @id_tarea, @id_profesor, @nombre, @descripcion, @calificacion, @fecha_asignada, @fecha_entrega, @activo, @extencion, @doc_tarea_docente, @doc_tarea_nino, @accion",
                parameters);

            TempData["SuccessMessage"] = "Tarea creada exitosamente.";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
            TempData["ErrorMessage"] = "Ocurrió un error al crear la tarea.";
            throw;
        }

        // Redirigir según el rol del usuario
        if (rolUsuarioLogueado == "Administrador")
            return RedirectToAction("Details_Admin", "Ninos", new { id = IdNino });

        if (rolUsuarioLogueado == "Docente")
            return RedirectToAction("Details_Docente", "Ninos", new { id = IdNino, idDocente = IdProfesor });

        return RedirectToAction("ListaNinos", "Asistencia");
    }*/

/*    [HttpPost]
    public async Task<IActionResult> Crear_Tarea(
        int IdNino,
        int IdProfesor,
        string NombreTarea,
        string Descripcion,
        DateTime FechaEntrega,
        IFormFile? DocTareaDocente,
        bool Activo)
    {
        if (IdNino == 0 || IdProfesor == 0)
        {
            return NotFound();
        }

        var rolUsuarioLogueado = User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .SingleOrDefault();
        
        if (DocTareaDocente != null)
        {
            using (var ms = new MemoryStream())
            {
                await DocTareaDocente.CopyToAsync(ms);

                // Validar el tamaño del archivo (5 MB máximo)
                if (DocTareaDocente.Length > 5242880) // 5 MB
                {
                    TempData["ErrorMessage"] = "El archivo supera el tamaño máximo permitido de 5 MB.";

                    // Redirigir según el rol del usuario
                    if (rolUsuarioLogueado == "Administrador")
                        return RedirectToAction("Details_Admin", "Ninos", new { id = IdNino });

                    if (rolUsuarioLogueado == "Docente")
                        return RedirectToAction("Details_Docente", "Ninos",
                            new { id = IdNino, idDocente = IdProfesor });
                }

                msBit = ms.ToArray();

                // Imprimir detalles del archivo convertido a bytes
                Console.WriteLine("Archivo en bytes (Base64, primeros 100 caracteres):");
                Console.WriteLine(Convert.ToBase64String(msBit).Substring(0, 100));

                Console.WriteLine($"Tamaño total en bytes: {msBit.Length}");

                Console.WriteLine("Primeros 10 bytes del archivo:");
                Console.WriteLine(string.Join(", ", msBit.Take(10)));
            }
        }

        Tareas Task = new Tareas()
        { 
            IdProfesor = IdProfesor,
            Nombre = NombreTarea,
            Descripcion = Descripcion,
            FechaAsignada = DateTime.Now,
            FechaEntrega = FechaEntrega,
            Activo = Activo
        };

        try
        {
            // Llamada al procedimiento almacenado
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarTareas @id_nino, @id_tarea, @id_profesor, @nombre, @descripcion, @calificacion, @fecha_asignada, @fecha_entrega, @activo, @extencion, @doc_tarea_docente, @doc_tarea_nino, @accion",
                new[]
                {
                    new SqlParameter("@id_nino", IdNino),
                    new SqlParameter("@id_tarea", DBNull.Value),
                    new SqlParameter("@id_profesor", Task.IdProfesor),
                    new SqlParameter("@nombre", Task.Nombre ?? (object)DBNull.Value),
                    new SqlParameter("@descripcion", Task.Descripcion ?? (object)DBNull.Value),
                    new SqlParameter("@calificacion", DBNull.Value),
                    new SqlParameter("@fecha_asignada", Task.FechaAsignada),
                    new SqlParameter("@fecha_entrega", Task.FechaEntrega),
                    new SqlParameter("@activo", Task.Activo),
                    new SqlParameter("@doc_tarea_docente", Task.DocTareaDocente ?? (object)DBNull.Value),
                    new SqlParameter("@doc_tarea_nino", DBNull.Value),
                    new SqlParameter("@accion", "AGREGAR")
                });

            TempData["SuccessMessage"] = "Tarea creada exitosamente.";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
            TempData["ErrorMessage"] = "Ocurrió un error al crear la tarea.";
            throw;
        }

        // Redirigir según el rol del usuario
        if (rolUsuarioLogueado == "Administrador")
            return RedirectToAction("Details_Admin", "Ninos", new { id = IdNino });

        if (rolUsuarioLogueado == "Docente")
            return RedirectToAction("Details_Docente", "Ninos", new { id = IdNino, idDocente = IdProfesor });

        return RedirectToAction("ListaNinos", "Asistencia");
    }
*/

    [HttpGet]
    public async Task<IActionResult> Edit_Tarea(int idNino, int idTarea, int? idDocente)
    {
        if (idNino == 0 || idTarea == 0) return NotFound();

        // Obtener la relación entre el niño y la tarea
        var relacionNinoTarea = await _context.RelNinoTarea
            .Include(rt => rt.Tareas)
            .FirstOrDefaultAsync(rt => rt.IdNino == idNino && rt.IdTarea == idTarea);

        if (relacionNinoTarea == null) return NotFound();

        var tarea = relacionNinoTarea.Tareas;

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
            ViewBag.RoleName = "Docente";
            ViewBag.DocenteId = docente.IdDocente;
        }
        else
        {
            ViewBag.RoleName = "Administrador";
        }

        // Pasar la calificación al ViewBag
        ViewBag.Calificacion = relacionNinoTarea.Calificacion;

        // Pasar la tarea al modelo
        ViewBag.IdNino = idNino;

        return View(tarea);
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
        if (nino == null) return NotFound();

        // Si el IdProfesor no se ha seleccionado (es null), asignamos el profesor actual de la tarea
        if (!idProfesor.HasValue) idProfesor = tarea.IdProfesor; // Asigna el profesor actual

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
            return RedirectToAction("Details_Admin", "Ninos", new { id = idNino });

        if (rolUsuarioLogueado == "Docente") return RedirectToAction("Details_Docente", "Ninos", new { id = idNino });

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
            return RedirectToAction("Details_Admin", "Ninos", new { id = idNino });

        if (rolUsuarioLogueado == "Docente") return RedirectToAction("Details_Docente", "Ninos", new { id = idNino });

        return RedirectToAction("ListaNinos", "Asistencia");
    }
}