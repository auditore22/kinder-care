using System.Diagnostics;
using kinder_care.Models;
using kinder_care.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace kinder_care.Controllers
{
    [Authorize] // Ensure only authenticated users can access these actions
    public class UsersController : Controller
    {
        private readonly ExpedienteService _expedienteService;
        private readonly ILogger<UsersController> _logger;

        // Constructor para inyectar los servicios necesarios
        public UsersController(ExpedienteService expedienteService, ILogger<UsersController> logger)
        {
            _expedienteService = expedienteService;
            _logger = logger;
        }

        // Retrieve and display detailed child records
        public async Task<IActionResult> ManageRecords()
        {
            ViewBag.CurrentSection = "ManageRecords"; // Set the current section for UI purposes
            var expedientes = await _expedienteService.GetExpedientesAsync(); // Fetch child records from the service
            return View(expedientes); // Pass records to the view
        }

        // Update a child's record with the provided data
        [HttpPost]
        public async Task<IActionResult> ActualizarExpedienteNino([FromBody] ExpedienteCompletoNino expediente)
        {
            try
            {
                // Validate required fields before proceeding
                if (string.IsNullOrEmpty(expediente.NombreNino) || string.IsNullOrEmpty(expediente.Direccion) ||
                    string.IsNullOrEmpty(expediente.Poliza))
                {
                    return BadRequest("Los campos obligatorios no pueden estar vacíos.");
                }

                await _expedienteService.UpdateExpedienteAsync(expediente); // Call service to update the record
                return Ok(); // Return success response
            }
            catch (SqlException ex)
            {
                // Log SQL-specific errors and return appropriate response
                _logger.LogError(ex, "SQL error occurred while updating the expediente.");
                return BadRequest($"Error SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors and return generic response
                _logger.LogError(ex, "Unexpected error occurred.");
                return BadRequest($"Error inesperado: {ex.Message}");
            }
        }

        // Manage a child's medical information (add/update)
        [HttpPost]
        public async Task<IActionResult> GestionarInformacionMedica([FromBody] InformacionMedicaRequest request)
        {
            try
            {
                await _expedienteService.GestionarInformacionMedicaAsync(request); // Call service to manage medical information
                return Ok(); // Return success response
            }
            catch (SqlException ex)
            {
                // Log SQL-specific errors and return appropriate response
                _logger.LogError(ex, "SQL error occurred while managing medical information.");
                return BadRequest($"Error SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors and return generic response
                _logger.LogError(ex, "Unexpected error occurred.");
                return BadRequest($"Error inesperado: {ex.Message}");
            }
        }

        // Manage a child's emergency contacts (add/update)
        [HttpPost]
        public async Task<IActionResult> GestionarContactosEmergencia([FromBody] ContactoEmergenciaRequest request)
        {
            try
            {
                // Validate required fields for emergency contacts
                if (string.IsNullOrEmpty(request.NombreContacto) || request.Telefono == null ||
                    string.IsNullOrEmpty(request.Relacion))
                {
                    return BadRequest("Los datos de contacto son obligatorios.");
                }

                await _expedienteService.GestionarContactosEmergenciaAsync(request); // Call service to manage emergency contact
                return Ok(); // Return success response
            }
            catch (SqlException ex)
            {
                // Log SQL-specific errors and return appropriate response
                _logger.LogError(ex, "SQL error occurred while managing emergency contacts.");
                return BadRequest($"Error SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors and return generic response
                _logger.LogError(ex, "Unexpected error occurred.");
                return BadRequest($"Error inesperado: {ex.Message}");
            }
        }

        // Handle application errors and display error view
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}