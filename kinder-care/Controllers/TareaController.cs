using System.Security.Claims;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    [HttpPost]
    public async Task<IActionResult> Crear_Tarea(
        int IdNino,
        int IdProfesor,
        string NombreTarea,
        string Descripcion,
        DateTime FechaEntrega,
        IFormFile? DocTareaDocente,
        bool Activo)
    {
        if (IdNino == 0 || IdProfesor == 0) return NotFound();

        var rolUsuarioLogueado = User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .SingleOrDefault();

        byte[]? fileData = null;
        int? idDoc = null;

        // Validar y procesar el archivo si se proporciona
        if (DocTareaDocente != null)
        {
            if (DocTareaDocente.Length > 5242880) // 5 MB
            {
                TempData["ErrorMessage"] = "El archivo supera el tamaño máximo permitido de 5 MB.";
                return RedirectToRoleBasedView(rolUsuarioLogueado, IdNino, IdProfesor);
            }

            using (var memoryStream = new MemoryStream())
            {
                await DocTareaDocente.CopyToAsync(memoryStream);
                fileData = memoryStream.ToArray();

                var doc = new Documentos
                {
                    Documento = fileData,
                    Nombre = NombreTarea + Path.GetExtension(DocTareaDocente.FileName), // "NombreTarea.extension"
                    Tipo = DocTareaDocente.ContentType // Obtenemos el tipo del archivo
                };

                _context.Documentos.Add(doc);
                await _context.SaveChangesAsync();

                // El ID del documento recién creado
                idDoc = doc.IdDoc;
            }
        }

        // Crear la tarea
        var tarea = new Tareas
        {
            IdProfesor = IdProfesor,
            Nombre = NombreTarea,
            Descripcion = Descripcion,
            FechaAsignada = DateTime.Now,
            FechaEntrega = FechaEntrega,
            Activo = Activo,
            IdDocDocente = idDoc
        };

        _context.Tareas.Add(tarea);
        await _context.SaveChangesAsync();

        // El ID de la tarea recién creada
        var idTarea = tarea.IdTarea;

        // Crear la relación niño-tarea
        var relNinoTarea = new RelNinoTarea
        {
            IdNino = IdNino,
            IdTarea = idTarea,
            Calificacion = 0
        };

        _context.RelNinoTarea.Add(relNinoTarea);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Tarea creada exitosamente.";

        return RedirectToRoleBasedView(rolUsuarioLogueado, IdNino, IdProfesor);
    }

    // Método auxiliar para redirigir basado en el rol
    private IActionResult RedirectToRoleBasedView(string? rolUsuarioLogueado, int IdNino, int IdProfesor)
    {
        if (rolUsuarioLogueado == "Administrador")
            return RedirectToAction("Details", "Ninos", new { id = IdNino });

        if (rolUsuarioLogueado == "Docente")
            return RedirectToAction("Details", "Ninos", new { id = IdNino, idDocente = IdProfesor });

        return RedirectToAction("ListaNinos", "Asistencia");
    }

    [HttpGet]
    public async Task<IActionResult> Edit_Tarea(int IdNino, int IdTarea, int? IdProfesor)
    {
        if (IdNino == 0 || IdProfesor == 0) return NotFound();

        // Obtener la relación entre el niño y la tarea
        var relacionNinoTarea = await _context.RelNinoTarea
            .Include(rt => rt.Tareas)
            .ThenInclude(td => td.IdDocDocenteNavigation)
            .FirstOrDefaultAsync(rt => rt.IdNino == IdNino && rt.IdTarea == IdTarea);

        if (relacionNinoTarea == null) return NotFound();

        var tarea = relacionNinoTarea.Tareas;

        var docTarea = tarea.IdDocDocenteNavigation;

        // Procesar el documento si existe
        string? base64Documento = null;
        string? tipoDocumento = null;
        string? nombreDocumento = null;

        // Verificar si el documento existe antes de intentar acceder a sus propiedades
        if (docTarea != null && docTarea.Documento != null)
        {
            base64Documento = Convert.ToBase64String(docTarea.Documento);
            tipoDocumento = docTarea.Tipo; // Tipo (ej. application/pdf, image/jpeg)
            nombreDocumento = docTarea.Nombre; // Nombre del archivo original
        }


        ViewBag.DocumentoBase64 = base64Documento; // Documento codificado en Base64
        ViewBag.DocumentoTipo = tipoDocumento; // Tipo MIME del documento
        ViewBag.DocumentoNombre = nombreDocumento; // Nombre del archivo

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
        ViewBag.IdNino = IdNino;

        return View(tarea);
    }

    [HttpPost]
    public async Task<IActionResult> Edit_Tarea(
        int IdTarea,
        int IdNino,
        int? IdProfesor,
        string? NombreTarea,
        string? Descripcion,
        int? Calificacion,
        DateTime? FechaAsignada,
        DateTime? FechaEntrega,
        bool? EliminarDocumento,
        IFormFile? DocTareaDocente)
    {
        if (IdTarea == 0 || IdNino == 0)
        {
            return NotFound();
        }

        // Obtener el rol del usuario logueado
        var rolUsuarioLogueado =
            User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

        // Buscar la tarea existente por id
        var tarea = await _context.Tareas.FindAsync(IdTarea);
        var RNT = await _context.RelNinoTarea.FindAsync(IdNino, IdTarea);

        if (tarea == null || RNT == null) return NotFound();

        // Procedimiento para editar la tarea:
        tarea.IdProfesor = IdProfesor ?? tarea.IdProfesor;
        tarea.Nombre = NombreTarea ?? tarea.Nombre;
        tarea.Descripcion = Descripcion ?? tarea.Descripcion;
        RNT.Calificacion = Calificacion ?? RNT.Calificacion;
        tarea.Activo = true;
        tarea.FechaAsignada = FechaAsignada ?? tarea.FechaAsignada;
        tarea.FechaEntrega = FechaEntrega ?? tarea.FechaEntrega;

        if (EliminarDocumento == true && DocTareaDocente == null && tarea.IdDocDocente != null)
        {
            var BuscarDocumento = await _context.Documentos.FindAsync(tarea.IdDocDocente);

            // Se settea la idDocDocente a null
            tarea.IdDocDocente = null;

            // Se elimina el documento
            _context.Documentos.Remove(BuscarDocumento);
        }

        byte[]? fileData = null;
        int? idDoc = null;

        // Validar y procesar el archivo si se proporciona
        if (DocTareaDocente != null)
        {
            if (DocTareaDocente.Length > 5242880) // 5 MB
            {
                TempData["ErrorMessageEditTarea"] = "El archivo supera el tamaño máximo permitido de 5 MB.";
                if (rolUsuarioLogueado == "Administrador")
                    return RedirectToAction("Details", "Ninos", new
                    {
                        id = IdNino
                    });
                if (rolUsuarioLogueado == "Docente")
                    return RedirectToAction("Details", "Ninos", new
                    {
                        id = IdNino
                    });
                return RedirectToAction("Details", "Ninos", new
                {
                    id = IdNino
                });
            }

            using (var memoryStream = new MemoryStream())
            {
                await DocTareaDocente.CopyToAsync(memoryStream);
                fileData = memoryStream.ToArray();

                var doc = new Documentos
                {
                    Documento = fileData,
                    Nombre = NombreTarea +
                             Path.GetExtension(DocTareaDocente.FileName), // "NombreTarea.extension"
                    Tipo = DocTareaDocente.ContentType // Obtenemos el tipo del archivo
                };

                if (tarea.IdDocDocente != null)
                {
                    var BuscarDocumento = await _context.Documentos.FindAsync(tarea.IdDocDocente);

                    // Se settea la idDocDocente a null
                    tarea.IdDocDocente = null;

                    // Se elimina el documento
                    _context.Documentos.Remove(BuscarDocumento);
                }

                _context.Documentos.Add(doc);

                await _context.SaveChangesAsync();

                // El ID del documento recién creado
                idDoc = doc.IdDoc;
            }
        }

        tarea.IdDocDocente = idDoc;

        await _context.SaveChangesAsync();

        TempData["SuccessMessageEditTarea"] = "Tarea actualizada exitosamente.";
        // Redirigir según el rol del usuario
        if (rolUsuarioLogueado == "Administrador")
            return RedirectToAction("Details", "Ninos", new
            {
                id = IdNino
            });
        if (rolUsuarioLogueado == "Docente")
            return RedirectToAction("Details", "Ninos", new
            {
                id = IdNino
            });
        return RedirectToAction("Details", "Ninos", new
        {
            id = IdNino
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrDeleteDocNino(
        int IdTarea,
        int IdNino,
        bool? EliminarDocumento,
        IFormFile? DocTareaNino)
    {
        if (IdTarea == 0 || IdNino == 0)
        {
            return NotFound();
        }

        // Obtener el rol del usuario logueado
        var rolUsuarioLogueado =
            User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

        // Buscar la tarea existente por id
        var tarea = await _context.Tareas.FindAsync(IdTarea);
        var RNT = await _context.RelNinoTarea.FindAsync(IdNino, IdTarea);
        var Nino = await _context.Ninos.FindAsync(IdNino);

        if (tarea == null || RNT == null) return NotFound();

        bool documentoEliminado = false;
        byte[]? fileData = null;
        int? idDoc = null;

        // En caso de que se elimine y no se suba nada
        if (EliminarDocumento == true && DocTareaNino == null && RNT.IdDocNino != null)
        {
            var BuscarDocumento = await _context.Documentos.FindAsync(RNT.IdDocNino);

            if (BuscarDocumento != null)
            {
                // Se settea la idDocNino a null
                RNT.IdDocNino = null;

                // Se elimina el documento
                _context.Documentos.Remove(BuscarDocumento);

                documentoEliminado = true; // Marcamos que se eliminó un documento
            }
        }

        // Validar y procesar el archivo si se proporciona
        if (DocTareaNino != null)
        {
            if (DocTareaNino.Length > 5242880) // 5 MB
            {
                TempData["ErrorMessageCreateOrDeleteDocNino"] = "El archivo supera el tamaño máximo permitido de 5 MB.";

                return RedirectToAction("Details", "Ninos", new { id = IdNino });
            }

            using (var memoryStream = new MemoryStream())
            {
                await DocTareaNino.CopyToAsync(memoryStream);
                fileData = memoryStream.ToArray();

                // Se le da formato a la fecha y hora, sin milisegundos
                string fechaHora = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                // Se sanitiza el nombre de la tarea para evitar caracteres problemáticos
                string nombreTarea = string.Concat(tarea.Nombre.Split(Path.GetInvalidFileNameChars()));

                // Construimos el nombre del archivo
                string nombreArchivo =
                    $"{Nino.NombreNino}_{nombreTarea}_{fechaHora}{Path.GetExtension(DocTareaNino.FileName)}";

                var doc = new Documentos
                {
                    Documento = fileData,
                    Nombre = nombreArchivo,
                    Tipo = DocTareaNino.ContentType
                };

                if (RNT.IdDocNino != null)
                {
                    var BuscarDocumento = await _context.Documentos.FindAsync(RNT.IdDocNino);

                    if (BuscarDocumento != null)
                    {
                        // Se settea la IdDocNino a null
                        RNT.IdDocNino = null;

                        // Se elimina el documento anterior
                        _context.Documentos.Remove(BuscarDocumento);
                    }
                }

                _context.Documentos.Add(doc);
                await _context.SaveChangesAsync();

                // El ID del documento recién creado
                idDoc = doc.IdDoc;
            }
        }

        RNT.IdDocNino = idDoc;
        await _context.SaveChangesAsync();

        // 🔹 Definir mensaje según la acción realizada
        if (documentoEliminado)
        {
            TempData["SuccessMessageCreateOrDeleteDocNino"] = "El documento de la tarea ha sido eliminado con éxito.";
        }
        else if (idDoc != null)
        {
            TempData["SuccessMessageCreateOrDeleteDocNino"] =
                "El documento de la tarea ha sido subido/modificado con éxito.";
        }

        return RedirectToAction("Details", "Ninos", new { id = IdNino });
    }


    [HttpPost]
    public async Task<IActionResult> Eliminar_Tarea(int idTarea, int idNino)
    {
        if (idTarea <= 0 || idNino <= 0) return NotFound();

        var buscarTarea = await _context.Tareas.FindAsync(idTarea);
        if (buscarTarea == null) return NotFound();

        var buscarDocTarea = await _context.Documentos.FindAsync(buscarTarea.IdDocDocente);

        var rolUsuarioLogueado = User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .SingleOrDefault();

        // Buscar la relación niño-tarea por claves primarias
        var BuscarRelTarea = await _context.RelNinoTarea.FindAsync(idNino, idTarea);

        if (BuscarRelTarea != null)
        {
            // Eliminar la relación niño-tarea
            _context.RelNinoTarea.Remove(BuscarRelTarea);

            // Eliminar la tarea
            _context.Tareas.Remove(buscarTarea);

            // Eliminar la tarea
            _context.Tareas.Remove(buscarTarea);

            if (buscarDocTarea != null)
                // Eliminar el doc de la tarea
                _context.Documentos.Remove(buscarDocTarea);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            TempData["SuccessMessageTareasEnProceso"] = "Tarea eliminada exitosamente.";
            return RedirectToRoleBasedView(rolUsuarioLogueado, idNino, buscarTarea.IdProfesor);
        }

        TempData["ErrorMessageTareasEnProceso"] = "Error al eliminar tarea.";
        return RedirectToRoleBasedView(rolUsuarioLogueado, idNino, buscarTarea.IdProfesor);
    }
}