using kinder_care.Models;
using kinder_care.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace kinder_care.Controllers
{
    public class AccessController : Controller
    {

        private readonly KinderCareContext _context;
        private readonly PasswordHasher<Usuarios> _passwordHasher;
        
        public AccessController(KinderCareContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuarios>();
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuarios usuario)
        {
            usuario.ContrasenaHash = _passwordHasher.HashPassword(usuario, usuario.ContrasenaHash);
            usuario.IdRol = 1;

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            if (usuario.IdUsuario != 0)
            {
                return RedirectToAction("Login", "Access");
            }
            else
            {
                ViewData["Mensaje"] = "No se pudo crear el usuario";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioVM usuario)
        {
            var usuarioRecibido = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.Nombre == usuario.Nombre);

            if (usuarioRecibido == null || !(usuarioRecibido.Activo ?? false))
            {
                ViewData["Mensaje"] = "Usuario no encontrado o inactivo";
                return View();
            }

            // Verificar si el rol es nulo
            if (usuarioRecibido.IdRolNavigation == null)
            {
                ViewData["Mensaje"] = "El rol del usuario no se encontró.";
                return View();
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(usuarioRecibido, usuarioRecibido.ContrasenaHash, usuario.ContrasenaHash);

            if (verificationResult != PasswordVerificationResult.Success)
            {
                ViewData["Mensaje"] = "Contraseña incorrecta";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioRecibido.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuarioRecibido.Nombre),
                new Claim(ClaimTypes.Role, usuarioRecibido.IdRolNavigation.Nombre)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties { AllowRefresh = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
