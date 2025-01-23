using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using kinder_care.Models;
using kinder_care.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace kinder_care.Controllers;

[Authorize]
public class ConfigurationController : Controller
{
    private readonly KinderCareContext _context;
    private readonly ILogger<ConfigurationController> _logger;

    public ConfigurationController(ILogger<ConfigurationController> logger, KinderCareContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Método para obtener el usuario actual
    private Usuarios ObtenerUsuarioActual()
    {
        // Verificamos si el usuario está autenticado
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            // Obtener el ID del usuario desde los claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                // Aquí, el método puede llamar a la base de datos para obtener el usuario completo
                // usando el ID del usuario actual.
                var userId = int.Parse(userIdClaim.Value);

                // Simulación de obtención del usuario desde la base de datos.
                // En un caso real, este valor sería obtenido mediante un servicio o DbContext.
                var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == userId);

                return usuario ?? throw new Exception("Usuario no encontrado");
            }
        }

        // Si no está autenticado, retornamos null o lanzamos una excepción.
        throw new Exception("Usuario no autenticado");
    }

    public ActionResult ChangePassword()
    {
        try
        {
            var usuario = ObtenerUsuarioActual();
            // Pasar el usuario a la vista
            return View(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el usuario actual");
            return RedirectToAction("Error");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult SaveProfile(ChangePasswordViewModel model)
    {
        _logger.LogInformation("Entrando al método SaveProfile.");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("El modelo no es válido.");
            return View("ChangePassword", model);
        }

        try
        {
            var usuario = ObtenerUsuarioActual();
            _logger.LogInformation("Usuario obtenido correctamente: {UserId}", usuario.IdUsuario);

            // Verificar la contraseña actual
            var passwordHasher = new PasswordHasher<Usuarios>();
            var passwordVerificationResult =
                passwordHasher.VerifyHashedPassword(usuario, usuario.ContrasenaHash, model.CurrentPassword);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("La contraseña actual es incorrecta.");
                ModelState.AddModelError("CurrentPassword", "La contraseña actual es incorrecta.");
                return View("ChangePassword", model);
            }

            // Validar nueva contraseña
            var regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,25}$");
            if (!regex.IsMatch(model.NewPassword))
            {
                _logger.LogWarning("La nueva contraseña no cumple con los requisitos.");
                ModelState.AddModelError("NewPassword",
                    "La nueva contraseña debe tener entre 8 y 25 caracteres, incluyendo al menos una letra y un número.");
                return View("ChangePassword", model);
            }

            // Validar que la nueva contraseña y su confirmación coincidan
            if (model.NewPassword != model.ConfirmPassword)
            {
                _logger.LogWarning("La nueva contraseña y su confirmación no coinciden.");
                ModelState.AddModelError("ConfirmPassword", "La nueva contraseña y su confirmación no coinciden.");
                return View("ChangePassword", model);
            }

            // Actualizar la contraseña
            usuario.ContrasenaHash = passwordHasher.HashPassword(usuario, model.NewPassword);
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();

            _logger.LogInformation("Contraseña cambiada exitosamente.");
            ViewBag.SuccessMessage = "Su contraseña ha sido cambiada exitosamente.";
            return View("ChangePassword", model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar la contraseña.");
            ModelState.AddModelError("", "Hubo un error al cambiar la contraseña. Intente de nuevo más tarde.");
            return View("ChangePassword", model);
        }
    }
}