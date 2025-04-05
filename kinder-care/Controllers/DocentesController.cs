using kinder_care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class DocentesController : Controller
{
    private readonly KinderCareContext _context;
    private readonly PasswordHasher<Docentes> _passwordHasher;

    public DocentesController(KinderCareContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<Docentes>();
    }

    //======================================================[VISTA INDEX]==========================================================================================
    public async Task<IActionResult> Index()
    {
        var docentes = await _context.Docentes
            .Include(u => u.IdUsuarioNavigation)
            .ToListAsync();

        return View(docentes);
    }

    //======================================================[VISTA DETAILS]==========================================================================================
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var docentes = await _context.Docentes
            .Include(d => d.IdUsuarioNavigation)
            .FirstOrDefaultAsync(m => m.IdDocente == id);
        if (docentes == null) return NotFound();

        return View(docentes);
    }

    //======================================================[VISTA EDIT]==========================================================================================
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var docentes = await _context.Docentes
            .Include(u => u.IdUsuarioNavigation)
            .FirstOrDefaultAsync(m => m.IdDocente == id);

        return View(docentes);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(int id, string Nombre, string Direccion, string CorreoElectronico,
        string NumTelefono, DateTime FechaNacimiento, string GrupoAsignado, bool Activo)
    {
        if (id == 0) return NotFound();

        var docentes = await _context.Docentes.FindAsync(id);
        if (docentes == null) return NotFound();

        var result = await _context.Database.ExecuteSqlRawAsync(
            "EXEC DocentesActualizar @id_Docente = {0}, @id_usuario = {1}, @nombre = {2}, @direccion = {3}, @correo_electronico = {4}, @num_Telefono = {5}, @fecha_nacimiento = {6}, @activo = {7}",
            id, docentes.IdUsuario, Nombre, Direccion, CorreoElectronico, NumTelefono, FechaNacimiento, Activo);

        if (result == 0) return NotFound();

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Docentes");
    }

    //======================================================[VISTA DELETE]==========================================================================================
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var docentes = await _context.Docentes
            .Include(d => d.IdUsuarioNavigation)
            .FirstOrDefaultAsync(m => m.IdDocente == id);
        if (docentes == null) return NotFound();

        return View(docentes);
    }

    //======================================================[PROBAR QUE UN USUARIO EXISTE]==========================================================================================
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var docentes = await _context.Docentes
            .Include(d => d.IdUsuarioNavigation)
            .FirstOrDefaultAsync(d => d.IdDocente == id);

        if (docentes != null)
        {
            docentes.IdUsuarioNavigation.IdRol = 3;
            _context.Docentes.Remove(docentes);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}