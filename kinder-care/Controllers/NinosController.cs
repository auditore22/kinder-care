using System.Security.Claims;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
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
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Verificar si el rol del usuario es 'Padre'
        var userRole = User.FindFirstValue(ClaimTypes.Role);

        if (userRole == "Padre")
        {
            // Consulta para obtener los niños relacionados con el padre logueado
            var ninos = await _context.RelPadresNinos
                .Where(r => r.IdPadre == userId) // Filtrar por el ID del padre logueado
                .Select(r => r.IdNino) // Seleccionar los IDs de los niños relacionados
                .Join(_context.Ninos, // Unir con la tabla de niños
                    rel => rel, // Clave externa de la tabla rel_padres_ninos (IdNino)
                    nino => nino.IdNino, // Clave primaria de la tabla ninos
                    (rel, nino) => nino) // Seleccionar los datos de los niños
                .ToListAsync();

            if (ninos.Any()) return View(ninos); // Mostrar la lista de niños si hay registros

            ViewBag.Mensaje = "No tienes niños asociados.";
            return View();
        }

        ViewBag.Mensaje = "No tienes acceso a esta información.";
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Crear_Nino()
    {
        // Obtener los niveles de grado
        var listNiveles = await _context.Niveles
            .Where(n => !string.IsNullOrEmpty(n.Nombre))
            .ToListAsync();

        if (listNiveles != null && listNiveles.Any())
        {
            // Asegura que el nivel actual del niño quede seleccionado
            ViewBag.ListaNiveles = new SelectList(listNiveles, "IdNivel", "Nombre");
        }
        else
        {
            ViewBag.ListaNiveles = new SelectList(Enumerable.Empty<SelectListItem>());
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Crear_Nino(string cedula, string nombreNino, DateTime fechaNacimiento,
        string direccion, string poliza, int idNivel, bool activo)
    {
        if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombreNino) || fechaNacimiento == default ||
            string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(poliza))
        {
            ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
            return View();
        }

        // Creación del objeto Nino
        var nino = new Ninos
        {
            Cedula = cedula,
            NombreNino = nombreNino,
            FechaNacimiento = fechaNacimiento,
            Direccion = direccion,
            Poliza = poliza,
            IdNivel = idNivel,
            Activo = activo
        };

        var verificarExistencia = await _context.Ninos.FirstOrDefaultAsync(n => n.Cedula == cedula);

        if (verificarExistencia != null)
        {
            ViewBag.ErrorMessage = "El nino ya ha sido registrado o esta inactivo";
            return View();
        }

        try
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC GestionarNino @IdNino = {0}, @Cedula = {1}, @NombreNino = {2}, @FechaNacimiento = {3}, @Direccion = {4}, @Poliza = {5}, @IdNivel = {6}, @Activo = {7}, @Accion = {8}",
                    null!,
                    nino.Cedula,
                    nino.NombreNino,
                    nino.FechaNacimiento,
                    nino.Direccion,
                    nino.Poliza,
                    nino.IdNivel,
                    nino.Activo,
                    "AGREGAR")
                ;

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error al registrar el niño: {ex.Message}";
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id, DateTime? fechaInicio, DateTime? fechaFin)
    {
        if (id == null) return NotFound();

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Verificar si el padre está asociado con este niño
        var isAssociated = await _context.RelPadresNinos
            .AnyAsync(r => r.IdPadre == userId && r.IdNino == id);

        if (!isAssociated)
        {
            ViewBag.Mensaje = "No tienes acceso a esta información.";
            return View();
        }

        // Obtener al niño y sus relaciones
        var nino = await _context.Ninos
            .Include(n => n.ProgresoAcademico) // Progreso académico
            .Include(n => n.ObservacionesDocentes) // Observaciones docentes
            .Include(n => n.Asistencia) // Asistencia
            .Include(n => n.RelNinoAlergia)
            .ThenInclude(ra => ra.Alergia) // Alergias
            .Include(n => n.RelNinoMedicamento)
            .ThenInclude(rm => rm.Medicamento) // Medicamentos
            .Include(n => n.RelNinoCondicion)
            .ThenInclude(rc => rc.Condicion) // Condiciones médicas
            .Include(n => n.RelNinoContactoEmergencia)
            .ThenInclude(re => re.ContactoEmergencia) // Contactos de emergencia
            .Include(n => n.RelNinoTarea)
            .ThenInclude(rt => rt.Tareas) // Tareas asignadas
            .Include(n => n.IdNivelNavigation)
            .Where(n => n.IdNino == id)
            .FirstOrDefaultAsync();

        if (nino == null) return NotFound();

        ViewBag.NombreNivel = await _context.Niveles.Where(idn => idn.IdNivel == nino.IdNivel).Select(nn => nn.Nombre)
            .FirstOrDefaultAsync();

        // Clasificar tareas según su estado
        var tareasEnProceso = nino.RelNinoTarea?
            .Where(rt => rt.Tareas.Activo && rt.Calificacion == 0)
            .Select(rt => rt.Tareas)
            .ToList() ?? new List<Tareas>();

        var tareasCompletadas = nino.RelNinoTarea?
            .Where(rt => rt.Tareas.Activo && rt.Calificacion > 0)
            .Select(rt => rt.Tareas)
            .ToList() ?? new List<Tareas>();

        var listDocentes = await _context.Usuarios
            .Where(d => d.Activo == true && d.IdRol == 2)
            .ToListAsync();

        ViewBag.ListaProfesores = new SelectList(listDocentes, "IdUsuario", "Nombre");

        // Pasar las tareas al ViewBag      
        ViewBag.TareasEnProceso = tareasEnProceso;
        ViewBag.TareasCompletadas = tareasCompletadas;

        // Pasar todas las opciones disponibles a la vista
        ViewBag.Asistencias = await _context.Asistencia.ToListAsync();
        ViewBag.Alergias = await _context.Alergias.ToListAsync();
        ViewBag.Medicamentos = await _context.Medicamentos.ToListAsync();
        ViewBag.CondicionesMedicas = await _context.CondicionesMedicas.ToListAsync();

        // Obtener contactos de emergencia relacionados
        var contactosRelacionados = nino.RelNinoContactoEmergencia
            .Select(r => r.ContactoEmergencia)
            .ToList();

        ViewBag.ContactosEmergencia = contactosRelacionados;

        var asistenciaNino = _context.Asistencia
            .Where(a => a.IdNino == id)
            .AsQueryable();

        if (fechaInicio.HasValue && fechaFin.HasValue)
            asistenciaNino = asistenciaNino.Where(a => a.Fecha >= fechaInicio.Value && a.Fecha <= fechaFin.Value);

        var asistencias = await asistenciaNino.ToListAsync();

        nino.Asistencia = asistencias;

        ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd")!;
        ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd")!;

        // Inicializa ContactosExistentes como un diccionario vacío si no hay contactos
        ViewBag.ContactosExistentes = contactosRelacionados.Any()
            ? contactosRelacionados.ToDictionary(c => c!.IdContactoEmergencia)!
            : new Dictionary<int, ContactosEmergencia>();


        return View(nino);
    }

    [HttpGet]
    public async Task<IActionResult> Details_Docente(int? id, int? idDocente, DateTime? fechaInicio, DateTime? fechaFin)
    {
        if (id == null) return NotFound();

        // Obtener el usuario actual y validar el docente
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);
        if (docente == null) return NotFound();

        // Verificar si el docente está asociado con este niño
        var isAssociated = await _context.RelDocenteNinoMateria
            .AnyAsync(r => r.IdDocente == docente.IdDocente && r.IdNino == id);

        if (!isAssociated)
        {
            ViewBag.Mensaje = "No tienes acceso a esta información.";
            return View();
        }

        // Obtener información del niño y sus relaciones
        var nino = await _context.Ninos
            .Include(n => n.ProgresoAcademico)
            .Include(n => n.ObservacionesDocentes)
            .Include(n => n.Asistencia)
            .Include(n => n.RelNinoAlergia).ThenInclude(ra => ra.Alergia)
            .Include(n => n.RelNinoMedicamento).ThenInclude(rm => rm.Medicamento)
            .Include(n => n.RelNinoCondicion).ThenInclude(rc => rc.Condicion)
            .Include(n => n.RelNinoContactoEmergencia).ThenInclude(re => re.ContactoEmergencia)
            .Include(n => n.RelNinoTarea).ThenInclude(rt => rt.Tareas)
            .FirstOrDefaultAsync(n => n.IdNino == id);

        if (nino == null) return NotFound();

        // Clasificar tareas según el estado
        var tareas = nino.RelNinoTarea?
            .Where(rt => rt.Tareas.Activo)
            .ToList();

        var tareasEnProceso = tareas?
            .Where(rt => rt.Calificacion == 0)
            .Select(rt => rt.Tareas)
            .ToList() ?? new List<Tareas>();

        var tareasCompletadas = tareas?
            .Where(rt => rt.Calificacion > 0)
            .Select(rt => rt.Tareas)
            .ToList() ?? new List<Tareas>();

        // Configurar ViewBag.DocenteId o ListaDocentes
        if (idDocente.HasValue)
        {
            ViewBag.DocenteId = idDocente.Value;
        }
        else
        {
            var listDocentes = await _context.Docentes
                .Include(d => d.IdUsuarioNavigation)
                .Where(d => d.Activo)
                .Select(d => new
                {
                    d.IdDocente,
                    NombreDocente = d.IdUsuarioNavigation.Nombre
                })
                .ToListAsync();

            ViewBag.ListaDocentes = (listDocentes.Any()
                ? new SelectList(listDocentes, "IdDocente", "NombreDocente")
                : null)!;
        }

        // Obtener los niveles de grado
        var listNiveles = await _context.Niveles
            .Where(n => !string.IsNullOrEmpty(n.Nombre))
            .ToListAsync();

        if (listNiveles != null && listNiveles.Any())
        {
            // Asegura que el nivel actual del niño quede seleccionado
            ViewBag.ListaNiveles = new SelectList(listNiveles, "IdNivel", "Nombre", nino.IdNivel);
        }
        else
        {
            ViewBag.ListaNiveles = new SelectList(Enumerable.Empty<SelectListItem>());
        }

        // Filtrar asistencia
        var asistenciaNino = _context.Asistencia
            .Where(a => a.IdNino == id)
            .AsQueryable();

        if (fechaInicio.HasValue && fechaFin.HasValue)
            asistenciaNino = asistenciaNino.Where(a => a.Fecha >= fechaInicio.Value && a.Fecha <= fechaFin.Value);

        nino.Asistencia = await asistenciaNino.ToListAsync();

        // Preparar ViewBags
        ViewBag.TareasEnProceso = tareasEnProceso;
        ViewBag.TareasCompletadas = tareasCompletadas;
        ViewBag.Asistencias = await _context.Asistencia.ToListAsync();
        ViewBag.Alergias = await _context.Alergias.ToListAsync();
        ViewBag.Medicamentos = await _context.Medicamentos.ToListAsync();
        ViewBag.CondicionesMedicas = await _context.CondicionesMedicas.ToListAsync();
        ViewBag.ContactosEmergencia = nino.RelNinoContactoEmergencia
            .Select(r => r.ContactoEmergencia)
            .ToList();

        ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd")!;
        ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd")!;

        return View(nino);
    }

    [HttpGet]
    public async Task<IActionResult> Details_Admin(int? id, DateTime? fechaInicio, DateTime? fechaFin)
    {
        if (id == null) return NotFound();

        var nino = await _context.Ninos
            .Include(n => n.ProgresoAcademico)
            .Include(n => n.ObservacionesDocentes)
            .Include(n => n.Asistencia)
            .Include(n => n.RelNinoAlergia).ThenInclude(ra => ra.Alergia)
            .Include(n => n.RelNinoMedicamento).ThenInclude(rm => rm.Medicamento)
            .Include(n => n.RelNinoCondicion).ThenInclude(rc => rc.Condicion)
            .Include(n => n.RelNinoContactoEmergencia).ThenInclude(re => re.ContactoEmergencia)
            .Include(n => n.RelNinoTarea).ThenInclude(rt => rt.Tareas)
            .ThenInclude(t => t.DocTareaDocente) // Asegúrate de incluir el documento del docente
            .Include(n => n.RelNinoTarea)
            .ThenInclude(rt => rt.DocTareaNino) // Asegúrate de incluir el documento del niño
            .FirstOrDefaultAsync(n => n.IdNino == id);

        if (nino == null) return NotFound();

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

        // Obtener los niveles de grado
        var listNiveles = await _context.Niveles
            .Where(n => !string.IsNullOrEmpty(n.Nombre))
            .ToListAsync();

        if (listNiveles != null && listNiveles.Any())
        {
            // Asegura que el nivel actual del niño quede seleccionado
            ViewBag.ListaNiveles = new SelectList(listNiveles, "IdNivel", "Nombre", nino.IdNivel);
        }
        else
        {
            ViewBag.ListaNiveles = new SelectList(Enumerable.Empty<SelectListItem>());
        }

        // Clasificar tareas según el estado
        var tareas = nino.RelNinoTarea?
            .Where(rt => rt.Tareas.Activo)
            .ToList();

        var tareasEnProceso = tareas?
            .Where(rt => rt.Calificacion == 0)
            .Select(rt => rt.Tareas)
            .ToList() ?? new List<Tareas>();

        var tareasCompletadas = tareas?
            .Where(rt => rt.Calificacion > 0)
            .Select(rt => rt.Tareas)
            .ToList() ?? new List<Tareas>();

        // Filtrar asistencia
        var asistenciaNino = _context.Asistencia
            .Where(a => a.IdNino == id)
            .AsQueryable();

        if (fechaInicio.HasValue && fechaFin.HasValue)
            asistenciaNino = asistenciaNino.Where(a => a.Fecha >= fechaInicio.Value && a.Fecha <= fechaFin.Value);

        nino.Asistencia = await asistenciaNino.ToListAsync();

        // Preparar ViewBags
        ViewBag.TareasEnProceso = tareasEnProceso;
        ViewBag.TareasCompletadas = tareasCompletadas;
        ViewBag.Asistencias = await _context.Asistencia.ToListAsync();
        ViewBag.Alergias = await _context.Alergias.ToListAsync();
        ViewBag.Medicamentos = await _context.Medicamentos.ToListAsync();
        ViewBag.CondicionesMedicas = await _context.CondicionesMedicas.ToListAsync();
        ViewBag.ContactosEmergencia = nino.RelNinoContactoEmergencia
            .Select(r => r.ContactoEmergencia)
            .ToList();

        ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd")!;
        ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd")!;

        return View(nino);
    }

    [HttpGet]
    public async Task<IActionResult> Details_Tareas(int idNino, int idTarea)
    {
        if (idNino <= 0 || idTarea <= 0) return NotFound();

        // Verificar existencia del niño y la tarea
        var verificarNino = await _context.Ninos.FindAsync(idNino);
        var verificarTarea = await _context.Tareas.FindAsync(idTarea);
        var listDocentes = await _context.Usuarios
            .Where(d => d.Activo == true && d.IdRol == 2)
            .ToListAsync();

        if (verificarNino == null || verificarTarea == null) return NotFound();

        // Verificar la relación entre niño y tarea
        var verificarRel = await _context.RelNinoTarea
            .Include(t => t.Tareas)
            .FirstOrDefaultAsync(tn => tn.IdNino == idNino && tn.IdTarea == idTarea);

        if (verificarRel == null) return NotFound();

        // Obtener detalles del profesor asignado
        var profesorTarea = await _context.Docentes.FindAsync(verificarRel.Tareas.IdProfesor);

        var profUsuario = await _context.Usuarios.FindAsync(profesorTarea!.IdUsuario);
        // Pasar datos a la vista mediante ViewBag
        ViewBag.Nino = verificarNino;
        ViewBag.Tarea = verificarRel.Tareas;
        ViewBag.Profesor = profUsuario!;
        ViewBag.ListaProfesores = new SelectList(listDocentes, "IdUsuario", "Nombre");

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var nino = await _context.Ninos.FirstOrDefaultAsync(n => n.IdNino == id);

        if (nino == null) return NotFound();

        return View(nino);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string nombreNino, string direccion, int idnivel, string poliza)
    {
        if (id == 0) return NotFound();

        var nino = await _context.Ninos.FindAsync(id);
        if (nino == null) return NotFound();

        var result = await _context.Database.ExecuteSqlRawAsync(
            "EXEC GestionarNino @IdNino = {0}, @Cedula = {1}, @NombreNino = {2}, @FechaNacimiento = {3}, @Direccion = {4}, @Poliza = {5}, @IdNivel = {6}, @Activo = {7}, @Accion = {8}",
            id, nino.Cedula, nombreNino, nino.FechaNacimiento, direccion, poliza, idnivel, nino.Activo!, "ACTUALIZAR");

        if (result == 0) return NotFound();

        var rolUsuarioLogueado = User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .SingleOrDefault();

        if (rolUsuarioLogueado == "Administrador") return RedirectToAction("Details_Admin", "Ninos", new { id });

        if (rolUsuarioLogueado == "Docente") return RedirectToAction("Details_Docente", "Ninos", new { id });

        return RedirectToAction("Details", "Ninos", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> Edit_Info_Medica(
        int id,
        int[] idAlergia,
        int[] idCondicion,
        int[] idMedicamento,
        string newAlergia,
        string newCondicion,
        string newMedicamento,
        string dosis)
    {
        if (id == 0) return NotFound();

        // Obtener relaciones actuales desde la base de datos
        var alergiasExistentes =
            _context.RelNinoAlergia.Where(ra => ra.IdNino == id).Select(ra => ra.IdAlergia).ToList();
        var condicionesExistentes =
            _context.RelNinoCondicion.Where(rc => rc.IdNino == id).Select(rc => rc.IdCondicion).ToList();
        var medicamentosExistentes = _context.RelNinoMedicamento.Where(rm => rm.IdNino == id)
            .Select(rm => rm.IdMedicamento).ToList();

        // Agregar nuevas alergias, condiciones y medicamentos si no existen en la base de datos
        if (!string.IsNullOrWhiteSpace(newAlergia))
        {
            var alergiaExistente = await _context.Alergias.FirstOrDefaultAsync(a => a.NombreAlergia == newAlergia);
            if (alergiaExistente == null)
            {
                alergiaExistente = new Alergias { NombreAlergia = newAlergia };
                _context.Alergias.Add(alergiaExistente);
                await _context.SaveChangesAsync();
            }

            idAlergia = idAlergia.Append(alergiaExistente.IdAlergia).ToArray();
        }

        if (!string.IsNullOrWhiteSpace(newCondicion))
        {
            var condicionExistente =
                await _context.CondicionesMedicas.FirstOrDefaultAsync(c => c.NombreCondicion == newCondicion);
            if (condicionExistente == null)
            {
                condicionExistente = new CondicionesMedicas { NombreCondicion = newCondicion };
                _context.CondicionesMedicas.Add(condicionExistente);
                await _context.SaveChangesAsync();
            }

            idCondicion = idCondicion.Append(condicionExistente.IdCondicionMedica).ToArray();
        }

        if (!string.IsNullOrWhiteSpace(newMedicamento))
        {
            var medicamentoExistente =
                await _context.Medicamentos.FirstOrDefaultAsync(m => m.NombreMedicamento == newMedicamento);
            if (medicamentoExistente == null)
            {
                medicamentoExistente = new Medicamentos
                    { NombreMedicamento = newMedicamento, Dosis = dosis ?? "No especificada" };
                _context.Medicamentos.Add(medicamentoExistente);
                await _context.SaveChangesAsync();
            }

            idMedicamento = idMedicamento.Append(medicamentoExistente.IdMedicamento).ToArray();
        }

        // Eliminar relaciones desmarcadas por el usuario
        foreach (var alergiaId in alergiasExistentes.Except(idAlergia))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_alergia = {1}, @accion = 'ELIMINAR'", id,
                alergiaId);
        foreach (var condicionId in condicionesExistentes.Except(idCondicion))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_condicion = {1}, @accion = 'ELIMINAR'", id,
                condicionId);
        foreach (var medicamentoId in medicamentosExistentes.Except(idMedicamento))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_medicamento = {1}, @accion = 'ELIMINAR'", id,
                medicamentoId);

        // Agregar relaciones nuevas seleccionadas por el usuario
        foreach (var alergiaId in idAlergia.Except(alergiasExistentes))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_alergia = {1}, @accion = 'AGREGAR'", id,
                alergiaId);
        foreach (var condicionId in idCondicion.Except(condicionesExistentes))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_condicion = {1}, @accion = 'AGREGAR'", id,
                condicionId);
        foreach (var medicamentoId in idMedicamento.Except(medicamentosExistentes))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_medicamento = {1}, @accion = 'AGREGAR'", id,
                medicamentoId);

        await _context.SaveChangesAsync();

        var rolUsuarioLogueado =
            User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

        if (rolUsuarioLogueado == "Administrador") return RedirectToAction("Details_Admin", "Ninos", new { id });

        if (rolUsuarioLogueado == "Docente") return RedirectToAction("Details_Docente", "Ninos", new { id });

        return RedirectToAction("Details", "Ninos", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> Edit_Contacto_Emergencia(int idNino,
        Dictionary<int, ContactosEmergencia> contactosExistentes, string newNombre, string newRelacion, int newTelefono,
        string newDireccion, int? eliminarContactoId)
    {
        if (idNino == 0) return NotFound();

        // Eliminar contactos existentes
        if (eliminarContactoId.HasValue)
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarContactosEmergencia @id_nino = {0}, @id_contacto = {1}, @accion = {2}",
                idNino, eliminarContactoId.Value, "ELIMINAR");


        // Actualizar contactos existentes
        foreach (var contacto in contactosExistentes.Values)
            if (contacto.IdContactoEmergencia != 0)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC GestionarContactosEmergencia @id_nino = {0}, @nombre_contacto = {1}, @telefono = {2}, @relacion = {3}, @direccion = {4}, @id_contacto = {5}, @accion = {6}",
                    idNino, contacto.NombreContacto, contacto.Telefono!, contacto.Relacion!,
                    contacto.Direccion!, contacto.IdContactoEmergencia, "ACTUALIZAR");

        await _context.SaveChangesAsync();

        var rolUsuarioLogueado =
            User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

        if (rolUsuarioLogueado == "Administrador")
            return RedirectToAction("Details_Admin", "Ninos", new { id = idNino });

        if (rolUsuarioLogueado == "Docente") return RedirectToAction("Details_Docente", "Ninos", new { id = idNino });

        return RedirectToAction("Details", "Ninos", new { id = idNino });
    }

    [HttpPost]
    public async Task<IActionResult> Edit_Contacto_Emergencia_2(int idNino,
        string newNombre, string newRelacion, int newTelefono,
        string newDireccion, int? eliminarContactoId)
    {
        // Agregar nuevo contacto si se ha ingresado información válida
        if (!string.IsNullOrWhiteSpace(newNombre) && !string.IsNullOrWhiteSpace(newRelacion) && newTelefono != 0 &&
            !string.IsNullOrWhiteSpace(newDireccion))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarContactosEmergencia @id_nino = {0}, @nombre_contacto = {1}, @telefono = {2}, @relacion = {3}, @direccion = {4}, @id_contacto = {5}, @accion = {6}",
                idNino, newNombre, newTelefono, newRelacion, newDireccion, null!, "AGREGAR");

        await _context.SaveChangesAsync();

        var rolUsuarioLogueado =
            User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

        if (rolUsuarioLogueado == "Administrador")
            return RedirectToAction("Details_Admin", "Ninos", new { id = idNino });

        if (rolUsuarioLogueado == "Docente") return RedirectToAction("Details_Docente", "Ninos", new { id = idNino });

        return RedirectToAction("Details", "Ninos", new { id = idNino });
    }
}