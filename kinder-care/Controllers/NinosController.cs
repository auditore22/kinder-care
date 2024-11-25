using System.Security.Claims;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

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
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        // Verificar si el padre está asociado con este niño
        var isAssociated = await _context.RelPadresNinos
            .AnyAsync(r => r.IdPadre == userId && r.IdNino == id);

        if (!isAssociated)
        {
            ViewBag.Mensaje = "No tienes acceso a esta información.";
            return View();
        }

        // Traer la información completa del niño junto con todas las relaciones
        var nino = await _context.Ninos
            .Include(n => n.ProgresoAcademico)           // Progreso académico
            .Include(n => n.ObservacionesDocentes)       // Observaciones de los docentes
            .Include(n => n.RelNinoAlergia)              // Alergias
            .ThenInclude(ra => ra.Alergia)
            .Include(n => n.RelNinoMedicamento)          // Medicamentos
            .ThenInclude(rm => rm.Medicamento)
            .Include(n => n.RelNinoCondicion)            // Condiciones médicas
            .ThenInclude(rc => rc.Condicion)
            .Include(n => n.RelNinoContactoEmergencia)   // Contactos de emergencia
            .ThenInclude(re => re.ContactoEmergencia)
            .Include(n => n.Asistencia)                  // Asistencias
            .FirstOrDefaultAsync(n => n.IdNino == id);

        if (nino == null) return NotFound();

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

        // Inicializa ContactosExistentes como un diccionario vacío si no hay contactos
        if (!contactosRelacionados.Any())
            ViewBag.ContactosExistentes = new Dictionary<int, ContactosEmergencia>();
        else
            ViewBag.ContactosExistentes = contactosRelacionados.ToDictionary(c => c.IdContactoEmergencia);

        return View(nino);
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
    public async Task<IActionResult> Edit(int id, string NombreNino, string Direccion, string Poliza)
    {
        if (id == 0) return NotFound();

        var nino = await _context.Ninos.FindAsync(id);
        if (nino == null) return NotFound();

        // Llamada al procedimiento almacenado para actualizar los datos principales del niño (Dirección y Poliza)
        var result = await _context.Database.ExecuteSqlRawAsync(
            "EXEC GestionarNino @IdNino = {0}, @Cedula = {1}, @NombreNino = {2}, @FechaNacimiento = {3}, @Direccion = {4}, @Poliza = {5}",
            id, nino.Cedula, NombreNino, nino.FechaNacimiento, Direccion, Poliza);

        if (result == 0) return NotFound();

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Ninos");
    }

    [HttpPost]
    public async Task<IActionResult> Edit_Info_Medica(
        int id,
        int[] IdAlergia,
        int[] IdCondicion,
        int[] IdMedicamento,
        string NewAlergia,
        string NewCondicion,
        string NewMedicamento,
        string Dosis)
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
        if (!string.IsNullOrWhiteSpace(NewAlergia))
        {
            var alergiaExistente = await _context.Alergias.FirstOrDefaultAsync(a => a.NombreAlergia == NewAlergia);
            if (alergiaExistente == null)
            {
                alergiaExistente = new Alergias { NombreAlergia = NewAlergia };
                _context.Alergias.Add(alergiaExistente);
                await _context.SaveChangesAsync();
            }

            IdAlergia = IdAlergia.Append(alergiaExistente.IdAlergia).ToArray();
        }

        if (!string.IsNullOrWhiteSpace(NewCondicion))
        {
            var condicionExistente =
                await _context.CondicionesMedicas.FirstOrDefaultAsync(c => c.NombreCondicion == NewCondicion);
            if (condicionExistente == null)
            {
                condicionExistente = new CondicionesMedicas { NombreCondicion = NewCondicion };
                _context.CondicionesMedicas.Add(condicionExistente);
                await _context.SaveChangesAsync();
            }

            IdCondicion = IdCondicion.Append(condicionExistente.IdCondicionMedica).ToArray();
        }

        if (!string.IsNullOrWhiteSpace(NewMedicamento))
        {
            var medicamentoExistente =
                await _context.Medicamentos.FirstOrDefaultAsync(m => m.NombreMedicamento == NewMedicamento);
            if (medicamentoExistente == null)
            {
                medicamentoExistente = new Medicamentos
                    { NombreMedicamento = NewMedicamento, Dosis = Dosis ?? "No especificada" };
                _context.Medicamentos.Add(medicamentoExistente);
                await _context.SaveChangesAsync();
            }

            IdMedicamento = IdMedicamento.Append(medicamentoExistente.IdMedicamento).ToArray();
        }

        // Eliminar relaciones desmarcadas por el usuario
        foreach (var alergiaId in alergiasExistentes.Except(IdAlergia))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_alergia = {1}, @accion = 'ELIMINAR'", id,
                alergiaId);
        foreach (var condicionId in condicionesExistentes.Except(IdCondicion))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_condicion = {1}, @accion = 'ELIMINAR'", id,
                condicionId);
        foreach (var medicamentoId in medicamentosExistentes.Except(IdMedicamento))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_medicamento = {1}, @accion = 'ELIMINAR'", id,
                medicamentoId);

        // Agregar relaciones nuevas seleccionadas por el usuario
        foreach (var alergiaId in IdAlergia.Except(alergiasExistentes))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_alergia = {1}, @accion = 'AGREGAR'", id,
                alergiaId);
        foreach (var condicionId in IdCondicion.Except(condicionesExistentes))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_condicion = {1}, @accion = 'AGREGAR'", id,
                condicionId);
        foreach (var medicamentoId in IdMedicamento.Except(medicamentosExistentes))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarInformacionMedicaNino @id_nino = {0}, @id_medicamento = {1}, @accion = 'AGREGAR'", id,
                medicamentoId);

        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Ninos");
    }

    [HttpPost]
    public async Task<IActionResult> Edit_Contacto_Emergencia(int IdNino,
        Dictionary<int, ContactosEmergencia> ContactosExistentes, string NewNombre, string NewRelacion, int NewTelefono,
        string NewDireccion, int? EliminarContactoId)
    {
        if (IdNino == 0) return NotFound();

        // Eliminar contactos existentes
        if (EliminarContactoId.HasValue)
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarContactosEmergencia @id_nino = {0}, @id_contacto = {1}, @accion = {2}",
                IdNino, EliminarContactoId.Value, "ELIMINAR");


        // Actualizar contactos existentes
        foreach (var contacto in ContactosExistentes.Values)
            if (contacto.IdContactoEmergencia != 0)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC GestionarContactosEmergencia @id_nino = {0}, @nombre_contacto = {1}, @telefono = {2}, @relacion = {3}, @direccion = {4}, @id_contacto = {5}, @accion = {6}",
                    IdNino, contacto.NombreContacto, contacto.Telefono, contacto.Relacion,
                    contacto.Direccion, contacto.IdContactoEmergencia, "ACTUALIZAR");

        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Ninos");
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit_Contacto_Emergencia_2(int IdNino,
        string NewNombre, string NewRelacion, int NewTelefono,
        string NewDireccion, int? EliminarContactoId)
    {
        // Agregar nuevo contacto si se ha ingresado información válida
        if (!string.IsNullOrWhiteSpace(NewNombre) && !string.IsNullOrWhiteSpace(NewRelacion) && NewTelefono != 0 &&
            !string.IsNullOrWhiteSpace(NewDireccion))
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarContactosEmergencia @id_nino = {0}, @nombre_contacto = {1}, @telefono = {2}, @relacion = {3}, @direccion = {4}, @id_contacto = {5}, @accion = {6}",
                IdNino, NewNombre, NewTelefono, NewRelacion, NewDireccion, null, "AGREGAR");

        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Ninos");
    }
}