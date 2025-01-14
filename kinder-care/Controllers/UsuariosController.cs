using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class UsuariosController : Controller
{
    private readonly KinderCareContext _context;
    private readonly PasswordHasher<Usuarios> _passwordHasher;

    public UsuariosController(KinderCareContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<Usuarios>();
    }

    //======================================================[VISTA INDEX]==========================================================================================
    public async Task<IActionResult> Index()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .ToListAsync();

        return View(usuarios);
    }

    //======================================================[VISTA DETAILS]==========================================================================================
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var usuarios = await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(m => m.IdUsuario == id);
        if (usuarios == null) return NotFound();

        ViewBag.verificarRol = usuarios.IdRol == 1;

        return View(usuarios);
    }

    //======================================================[VISTA CREATE]==========================================================================================
    public async Task<IActionResult> Create()
    {
        ViewData["IdRol"] = new SelectList(await _context.Roles.ToListAsync(), "IdRol", "Nombre");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Usuarios usuario)
    {
        usuario.ContrasenaHash = _passwordHasher.HashPassword(usuario, usuario.ContrasenaHash);

        if (usuario.IdRol == 0)
        {
            var sinRol = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == "Padre");
            if (sinRol != null) usuario.IdRol = sinRol.IdRol;
        }

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC UsuariosCrear @Cedula = {0}, @Nombre = {1}, @ContrasenaHash = {2}, @NumTelefono = {3}, @Direccion = {4}, @CorreoElectronico = {5}, @IdRol = {6}, @Activo = {7}",
            usuario.Cedula,
            usuario.Nombre,
            usuario.ContrasenaHash,
            usuario.NumTelefono,
            usuario.Direccion,
            usuario.CorreoElectronico,
            usuario.IdRol,
            usuario.Activo
        );


        return RedirectToAction("Index", "Usuarios");
    }


    //======================================================[FUNCION EDIT]==========================================================================================
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var usuarios = await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(m => m.IdUsuario == id);

        ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Nombre", usuarios!.IdRol);
        return View(usuarios);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string nombre, string direccion, string correoElectronico,
        bool activo, int idRol)
    {
        if (id == 0) return NotFound();

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        var result = await _context.Database.ExecuteSqlRawAsync(
            "EXEC UsuariosActualizar @id_usuario = {0}, @nombre = {1}, @correo_electronico = {2}, @direccion = {3}, @activo = {4}, @id_rol = {5}",
            id, nombre, correoElectronico, direccion, activo, idRol);

        if (result == 0) return NotFound();

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Usuarios");
    }

    //======================================================[FUNCION DELETE]==========================================================================================
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var usuarios = await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(m => m.IdUsuario == id);
        if (usuarios == null) return NotFound();

        return View(usuarios);
    }
}