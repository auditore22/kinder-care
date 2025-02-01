using System.Diagnostics;
using System.Security.Claims;
using kinder_care.Models;
using kinder_care.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers
{
    [Authorize] // Ensure only authenticated users can access these actions
    public class UsersController : Controller
    {
        private readonly ExpedienteService _expedienteService;
        private readonly ILogger<UsersController> _logger;
        private readonly KinderCareContext _context;


        public UsersController(ExpedienteService expedienteService, ILogger<UsersController> logger,
            KinderCareContext context)
        {
            _expedienteService = expedienteService;
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Administrador, Docente")]
        public async Task<IActionResult> ManageRecords(int pageNumber = 1)
        {
            ViewBag.CurrentSection = "ManageRecords";

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.IdUsuario == userId);
            ViewBag.DocenteId = docente?.IdDocente!;

            List<ExpedienteCompletoNino> expedientes;

            if (userRole == "Docente" && docente != null)
            {
                var estudiantesIds = await _context.RelDocenteNinoMateria
                    .Where(r => r.IdDocente == docente.IdDocente)
                    .Select(r => r.IdNino)
                    .ToListAsync();

                if (!estudiantesIds.Any())
                {
                    ViewBag.Mensaje = "No hay expedientes relacionados con este docente.";
                    return View(new List<ExpedienteCompletoNino>());
                }

                expedientes = await _expedienteService.GetExpedientesByNinoIdsAsync(estudiantesIds);
            }
            else
            {
                expedientes = await _expedienteService.GetExpedientesAsync();
            }

            // Total de registros
            var totalItems = expedientes.Count;

            // Asignar valores a ViewBag
            ViewBag.TotalItems = totalItems;

            return View(expedientes);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarExpedienteNino([FromBody] ExpedienteCompletoNino expediente)
        {
            try
            {
                if (string.IsNullOrEmpty(expediente.NombreNino) || string.IsNullOrEmpty(expediente.Direccion) ||
                    string.IsNullOrEmpty(expediente.Poliza))
                {
                    return BadRequest("Los campos obligatorios no pueden estar vacíos.");
                }

                await _expedienteService.UpdateExpedienteAsync(expediente);
                return Ok();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error occurred while updating the expediente.");
                return BadRequest($"Error SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return BadRequest($"Error inesperado: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GestionarInformacionMedica([FromBody] InformacionMedicaRequest request)
        {
            try
            {
                await _expedienteService.GestionarInformacionMedicaAsync(request);
                return Ok();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error occurred while managing medical information.");
                return BadRequest($"Error SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return BadRequest($"Error inesperado: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GestionarContactosEmergencia([FromBody] ContactoEmergenciaRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.NombreContacto) || request.Telefono == null ||
                    string.IsNullOrEmpty(request.Relacion))
                {
                    return BadRequest("Los datos de contacto son obligatorios.");
                }

                await _expedienteService.GestionarContactosEmergenciaAsync(request);
                return Ok();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error occurred while managing emergency contacts.");
                return BadRequest($"Error SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return BadRequest($"Error inesperado: {ex.Message}");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}