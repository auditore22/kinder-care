using System.Net.Mail;
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
            // Hashear la contraseña del usuario
            usuario.ContrasenaHash = _passwordHasher.HashPassword(usuario, usuario.ContrasenaHash);
            // usuario.IdRol = 1;
            usuario.IdRol = 2;
            // usuario.IdRol = 3;

            // Generar un token aleatorio para TokenRecovery
            usuario.TokenRecovery = Guid.NewGuid().ToString() + "-" + new Random().Next(100000, 999999).ToString();

            // Establecer la fecha de creación y última actualización
            usuario.FechaCreacion = DateTime.Now;
            usuario.UltimaActualizacion = DateTime.Now;

            // Agregar el usuario a la base de datos
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
                .FirstOrDefaultAsync(u => u.CorreoElectronico == usuario.CorreoElectronico);

            if (usuarioRecibido == null || !usuarioRecibido.Activo)
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

            var verificationResult = _passwordHasher.VerifyHashedPassword(usuarioRecibido,
                usuarioRecibido.ContrasenaHash, usuario.ContrasenaHash);

            if (verificationResult != PasswordVerificationResult.Success)
            {
                ViewData["Mensaje"] = "Contraseña incorrecta";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioRecibido.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuarioRecibido.Nombre),
                new Claim(ClaimTypes.Email, usuarioRecibido.CorreoElectronico),
                new Claim(ClaimTypes.Role, usuarioRecibido.IdRolNavigation.Nombre)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties { AllowRefresh = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        // GET: Recovery
        [HttpGet]
        public IActionResult Recovery()
        {
            return View(new RecoveryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Recovery(RecoveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Buscar el usuario en la base de datos por correo electrónico
                var user = await _context.Usuarios
                    .SingleOrDefaultAsync(u => u.CorreoElectronico == model.Email);

                if (user == null)
                {
                    // Mostrar mensaje de error si el usuario no está registrado
                    ViewBag.ErrorMessage = "La dirección de correo no está registrada.";
                    return View(model);
                }

                // Generar un token de recuperación único utilizando Guid y un número aleatorio
                var token = Guid.NewGuid().ToString() + "-" + new Random().Next(100000, 999999).ToString();
                user.TokenRecovery = token;
                user.UltimaActualizacion = DateTime.Now;

                // Guardar el token en la base de datos
                _context.Update(user);
                await _context.SaveChangesAsync();

                // Verificar si el token fue guardado correctamente
                var updatedUser = await _context.Usuarios
                    .SingleOrDefaultAsync(u => u.IdUsuario == user.IdUsuario);

                if (updatedUser?.TokenRecovery == null || updatedUser.TokenRecovery != token)
                {
                    ViewBag.ErrorMessage =
                        "Hubo un problema al generar el token de recuperación. Por favor, inténtelo de nuevo.";
                    return View(model);
                }

                // Crear el enlace de recuperación
                var resetLink = Url.Action("ResetPassword", "Access",
                    new { email = model.Email, token = user.TokenRecovery },
                    Request.Scheme);

                // Enviar el correo de recuperación
                var emailSent = await SendRecoveryEmail(model.Email, resetLink);

                if (emailSent)
                {
                    ViewBag.SuccessMessage = "Se ha enviado un correo de recuperación.";
                    return View("RecoveryConfirmation");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hubo un problema al enviar el correo. Intenta de nuevo.";
                }
            }

            return View(model);
        }

        // GET: ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            // Validar el token de recuperación
            var user = _context.Usuarios
                .SingleOrDefault(u => u.CorreoElectronico == email && u.TokenRecovery == token);

            if (user == null)
            {
                ViewBag.ErrorMessage = "El enlace de recuperación no es válido.";
                return View("Recovery");
            }

            // Mostrar la vista para restablecer la contraseña
            var model = new ResetPasswordViewModel { Email = email, Token = token };
            return View(model);
        }

        // POST: ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Buscar el usuario por el correo y el token
                var user = await _context.Usuarios
                    .SingleOrDefaultAsync(u => u.CorreoElectronico == model.Email && u.TokenRecovery == model.Token);

                if (user == null)
                {
                    ViewBag.ErrorMessage = "El enlace de recuperación no es válido.";
                    return View("Recovery");
                }

                // Usar PasswordHasher para hashear la nueva contraseña
                user.ContrasenaHash = _passwordHasher.HashPassword(user, model.NewPassword);
                user.TokenRecovery = Guid.NewGuid().ToString() + "-" + new Random().Next(100000, 999999).ToString();
                user.UltimaActualizacion = DateTime.Now;

                _context.Update(user);
                await _context.SaveChangesAsync();

                ViewBag.SuccessMessage = "La contraseña se ha restablecido correctamente.";
                return RedirectToAction("Login", "Access");
            }

            return View(model);
        }

        // Función para enviar el correo de recuperación
        private async Task<bool> SendRecoveryEmail(string email, string resetLink)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress("no.reply.kindercare@gmail.com"),
                    Subject = "Recuperación de Contraseña - Kinder Care",
                    Body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                    <div style='max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);'>
                        <h2 style='color: #333;'>Recuperación de Contraseña</h2>
                        <p>Hola,</p>
                        <p>Recibimos una solicitud para restablecer tu contraseña en <b>Kinder Care</b>. Si no solicitaste este cambio, puedes ignorar este correo electrónico.</p>
                        <p>Para restablecer tu contraseña, haz clic en el siguiente enlace:</p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{resetLink}' style='background-color: #28a745; color: #fff; text-decoration: none; padding: 10px 20px; border-radius: 5px;'>Restablecer Contraseña</a>
                        </div>
                        <p>O copia y pega el siguiente enlace en tu navegador:</p>
                        <p style='word-break: break-all;'>{resetLink}</p>
                        <p>Este enlace es válido por 24 horas.</p>
                        <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;' />
                        <p style='font-size: 12px; color: #666;'>Si tienes alguna pregunta o necesitas ayuda, por favor contacta con nuestro equipo de soporte.</p>
                        <p style='font-size: 12px; color: #666;'>© 2024 Kinder Care. Todos los derechos reservados.</p>
                    </div>
                </body>
                </html>",
                    IsBodyHtml = true
                };
                mail.To.Add(email);

                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.Credentials =
                        new System.Net.NetworkCredential("no.reply.kindercare@gmail.com", "nqjf dtac vlkp shor");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                return false;
            }
        }
    }
}