using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly KinderCareContext _context;

    public UsersController(KinderCareContext context)
    {
        _context = context;
    }

    public ActionResult ManageRoles()
    {
        ViewBag.CurrentSection = "ManageRoles";
        return RedirectToAction("Index", "Usuarios"); //Redirigir al controller de Usuarios
    }

    public ActionResult ManageProfiles()
    {
        ViewBag.CurrentSection = "ManageProfiles";
        return RedirectToAction("Index", "Docentes"); //Redirigir al controller de Usuarios
    }

    public async Task<IActionResult> ManageRecords()
    {
        ViewBag.CurrentSection = "ManageRecords";

        // Consulta de la vista 'vw_ExpedienteCompletoNino'
        var expedientes = await _context.VwExpedienteCompletoNino
            .Select(e => new ExpedienteCompletoNino
            {
                IdNino = e.IdNino,
                Cedula = e.Cedula,
                NombreNino = e.NombreNino,
                FechaNacimiento = e.FechaNacimiento,
                Direccion = e.Direccion,
                Poliza = e.Poliza,
                NombreAlergia = e.NombreAlergia,
                NombreCondicion = e.NombreCondicion,
                NombreMedicamento = e.NombreMedicamento,
                Dosis = e.Dosis,
                NombreContacto = e.NombreContacto,
                TelefonoContacto = e.TelefonoContacto,
                RelacionContacto = e.RelacionContacto
            })
            .ToListAsync();

        return View(expedientes);
    }

    [HttpPost]
    public async Task<IActionResult> ActualizarExpedienteNino([FromBody] ExpedienteCompletoNino expediente)
    {
        try
        {
            if (string.IsNullOrEmpty(expediente.NombreNino) || string.IsNullOrEmpty(expediente.Direccion) || string.IsNullOrEmpty(expediente.Poliza))
            {
                return BadRequest("Los campos obligatorios no pueden estar vacíos.");
            }

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC ActualizarExpedienteNino @id_nino, @nombre_nino, @direccion, @poliza",
                new SqlParameter("@id_nino", expediente.IdNino),
                new SqlParameter("@nombre_nino", expediente.NombreNino),
                new SqlParameter("@direccion", expediente.Direccion),
                new SqlParameter("@poliza", expediente.Poliza)
            );
            return Ok();
        }
        catch (SqlException ex)
        {
            // Manejo detallado de excepciones SQL
            return BadRequest($"Error SQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error inesperado: {ex.Message}");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> GestionarInformacionMedica([FromBody] InformacionMedicaRequest request)
    {
        try
        {
            if (request.IdAlergia.HasValue || request.IdCondicion.HasValue || request.IdMedicamento.HasValue)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC GestionarInformacionMedicaNino @id_nino, @id_alergia, @id_condicion, @id_medicamento, @accion",
                    new SqlParameter("@id_nino", request.IdNino),
                    new SqlParameter("@id_alergia", request.IdAlergia ?? (object)DBNull.Value),
                    new SqlParameter("@id_condicion", request.IdCondicion ?? (object)DBNull.Value),
                    new SqlParameter("@id_medicamento", request.IdMedicamento ?? (object)DBNull.Value),
                    new SqlParameter("@accion", request.Accion)
                );
            }
            return Ok();
        }
        catch (SqlException ex)
        {
            // Manejo detallado de excepciones SQL
            return BadRequest($"Error SQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error inesperado: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> GestionarContactosEmergencia([FromBody] ContactoEmergenciaRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.NombreContacto) || request.Telefono == null || string.IsNullOrEmpty(request.Relacion))
            {
                return BadRequest("Los datos de contacto son obligatorios.");
            }

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC GestionarContactosEmergencia @id_nino, @nombre_contacto, @telefono, @relacion, @id_contacto, @accion",
                new SqlParameter("@id_nino", request.IdNino),
                new SqlParameter("@nombre_contacto", request.NombreContacto ?? (object)DBNull.Value),
                new SqlParameter("@telefono", request.Telefono ?? (object)DBNull.Value),
                new SqlParameter("@relacion", request.Relacion ?? (object)DBNull.Value),
                new SqlParameter("@id_contacto", request.IdContacto ?? (object)DBNull.Value),
                new SqlParameter("@accion", request.Accion)
            );
            return Ok();
        }
        catch (SqlException ex)
        {
            // Manejo detallado de excepciones SQL
            return BadRequest($"Error SQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error inesperado: {ex.Message}");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}