using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kinder_care.Models;
using System.Security.Claims;
using kinder_care.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace kinder_care.Controllers;

[Authorize]
public class ConfigurationController : Controller
{
    private readonly ILogger<ConfigurationController> _logger;
    private readonly KinderCareContext _context;

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
                int userId = int.Parse(userIdClaim.Value);

                // Simulación de obtención del usuario desde la base de datos.
                // En un caso real, este valor sería obtenido mediante un servicio o DbContext.
                var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == userId);

                return usuario ?? throw new Exception("Usuario no encontrado");
            }
        }
        // Si no está autenticado, retornamos null o lanzamos una excepción.
        throw new Exception("Usuario no autenticado");
    }

    public ActionResult UserProfile()
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
        if (!ModelState.IsValid)
        {
            return View("UserProfile", model); // Regresa a la vista con los errores de validación
        }

        // Obtener el usuario actual
        var usuario = ObtenerUsuarioActual();

        // Verificación de la contraseña actual
        var passwordHasher = new PasswordHasher<Usuarios>();
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(usuario, usuario.ContrasenaHash, model.CurrentPassword);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("CurrentPassword", "La contraseña actual es incorrecta.");
            return View("UserProfile", model);
        }

        // Validar nueva contraseña
        var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,25}$");
        if (!regex.IsMatch(model.NewPassword))
        {
            ModelState.AddModelError("NewPassword", "La contraseña debe tener entre 8 y 25 caracteres, también contener mínimo una letra y un número.");
            return View("UserProfile", model);
        }

        // Validar que la nueva contraseña y su confirmación coincidan
        if (model.NewPassword != model.ConfirmPassword)
        {
            ModelState.AddModelError("ConfirmPassword", "La nueva contraseña y su confirmación no coinciden.");
            return View("UserProfile", model);
        }

        // Actualizar la contraseña (hasheada) en la base de datos
        usuario.ContrasenaHash = passwordHasher.HashPassword(usuario, model.NewPassword);
        _context.SaveChanges();
        
        // Si la contraseña se cambió correctamente:
        TempData["SuccessMessage"] = "Su contraseña ha sido cambiada exitosamente.";
        return RedirectToAction("UserProfile"); // Redirige a la vista del perfil de usuario
    }
}