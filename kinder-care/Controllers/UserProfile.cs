using kinder_care.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

public class UserProfile : Controller
{
    private readonly KinderCareContext _context;
    private readonly PasswordHasher<Usuarios> _passwordHasher;

    public UserProfile(KinderCareContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<Usuarios>();
    }

    public async Task<IActionResult> Index(int? id)
    {
        if (id == null) return NotFound();

        var usuarios = await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(m => m.IdUsuario == id);
        if (usuarios == null) return NotFound();

        return View(usuarios);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string Nombre, string Direccion, string CorreoElectronico,
        string RoleName)
    {
        if (id == 0) return NotFound();

        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null) return NotFound();

        if (Nombre != usuario.Nombre || Direccion != usuario.Direccion || CorreoElectronico != usuario.CorreoElectronico)
        {
            if (RoleName == "Padre" || RoleName == "Docente")
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC GestionarUsuario2 @IdUsuario = {0}, @Cedula = {1}, @Nombre = {2}, @Num_Telefono = {3}, @Direccion = {4}, @CorreoElectronico = {5}, @IdRol = {6}, @Activo = {7}, @Accion = {8}",
                    usuario.IdUsuario, usuario.Cedula, Nombre, usuario.NumTelefono, Direccion, CorreoElectronico,
                    usuario.IdRol, usuario.Activo, "ACTUALIZAR");

                if (result == 0) return NotFound();

                return RedirectToAction("Index", "UserProfile", new { id = id });
            }
        }

        return RedirectToAction("Index", "UserProfile", new { id = id });
    }
}