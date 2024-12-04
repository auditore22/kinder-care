using kinder_care.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

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

        return View(usuarios);
    }

    //======================================================[VISTA CREATE]==========================================================================================
    public IActionResult Create()
    {
        ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Nombre");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Usuarios usuarios)
    {
        usuarios.ContrasenaHash = _passwordHasher.HashPassword(usuarios, usuarios.ContrasenaHash);
        usuarios.TokenRecovery = usuarios.TokenRecovery ?? Guid.NewGuid().ToString();

        await _context.Usuarios.AddAsync(usuarios);
        await _context.SaveChangesAsync();

        if (usuarios.IdUsuario != 0) return RedirectToAction(nameof(Index));

        ViewData["Mensaje"] = "No se pudo crear el usuario";
        return View(usuarios);
    }

    //======================================================[VISTA EDIT]==========================================================================================
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var usuarios = await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(m => m.IdUsuario == id);

        ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Nombre", usuarios.IdRol);
        return View(usuarios);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(int id, string Nombre, string Direccion, string CorreoElectronico,
        bool Activo, int IdRol)
    {
        if (id == 0) return NotFound();

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        var result = await _context.Database.ExecuteSqlRawAsync(
            "EXEC UsuariosActualizar @id_usuario = {0}, @nombre = {1}, @correo_electronico = {2}, @direccion = {3}, @activo = {4}, @id_rol = {5}",
            id, Nombre, CorreoElectronico, Direccion, Activo, IdRol);

        if (result == 0) return NotFound();

        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Usuarios", new {id = id});
    }

    //======================================================[VISTA DELETE]==========================================================================================
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var usuarios = await _context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(m => m.IdUsuario == id);
        if (usuarios == null) return NotFound();

        return View(usuarios);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var usuarios = await _context.Usuarios.FindAsync(id);
        if (usuarios != null)
        {
            var docentes = await _context.Docentes.Where(d => d.IdUsuario == id).ToListAsync();
            if (docentes.Any()) _context.Docentes.RemoveRange(docentes);

            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}