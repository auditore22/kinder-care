using kinder_care.Models;
using kinder_care.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace kinder_care.Controllers;

[Authorize]
public class PaymentsController : Controller
{
    private readonly KinderCareContext _context;

    public PaymentsController(KinderCareContext context)
    {
        _context = context;
    }

    // Mostrar la lista de pagos
    public async Task<IActionResult> ManagePayments()
    {
        // Obtén solo los usuarios con rol 3 (Padres)
        var padres = await _context.Usuarios
            .Where(u => u.IdRol == 3)
            .ToListAsync();

        // Obtén los niños registrados
        var ninos = await _context.Ninos
            .Where(n => n.Activo == true) // Asegúrate de que solo se muestren los niños activos
            .ToListAsync();

        // Obtén los tipos de pago activos
        var tiposPago = await _context.TipoPagos
            .Where(tp => tp.Activo == true)
            .ToListAsync();

        // Pasar las listas a la vista
        ViewBag.Padres = new SelectList(padres, "IdUsuario", "Nombre");
        ViewBag.Ninos =
            new SelectList(ninos, "IdNino",
                "NombreCompleto"); // Asegúrate de que "NombreCompleto" sea la propiedad correcta
        ViewBag.TiposPago = new SelectList(tiposPago, "IdTipoPago", "NombreTipoPago");

        return View();
    }
    [HttpGet]
    public async Task<IActionResult> CreatePayment()
    {
        var ninos = await _context.Ninos
            .Where(n => n.Activo)
            .ToListAsync();

        var padres = await _context.Usuarios
            .Where(u => u.IdRol == 3)
            .ToListAsync();

        var tiposPago = await _context.TipoPagos
            .Where(tp => tp.Activo)
            .ToListAsync();

        ViewBag.Ninos = new SelectList(ninos, "IdNino", "NombreNino");
        ViewBag.Padres = new SelectList(padres, "IdUsuario", "Nombre");
        ViewBag.TiposPago = new SelectList(tiposPago, "IdTipoPago", "NombreTipoPago");

        return View(new PaymentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePayment(PaymentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var nuevoPago = new Pagos
            {
                IdNino = model.IdNino,
                IdPadre = model.IdPadre,
                IdTipoPago = model.IdTipoPago,
                FechaPago = model.FechaPago,
                Monto = model.Monto,
                MetodoPago = model.MetodoPago,
                ReferenciaFactura = model.ReferenciaFactura,
                FechaCreacion = DateTime.Now,
                UltimaActualizacion = DateTime.Now
            };

            _context.Pagos.Add(nuevoPago);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("ManagePayments");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al registrar el pago: {ex.Message}";
            }
        }

        // Cargar nuevamente las listas para la vista
        var ninos = await _context.Ninos
            .Where(n => n.Activo)
            .ToListAsync();
        var padres = await _context.Usuarios
            .Where(u => u.IdRol == 3)
            .ToListAsync();
        var tiposPago = await _context.TipoPagos
            .Where(tp => tp.Activo)
            .ToListAsync();

        ViewBag.Ninos = new SelectList(ninos, "IdNino", "NombreNino");
        ViewBag.Padres = new SelectList(padres, "IdUsuario", "Nombre");
        ViewBag.TiposPago = new SelectList(tiposPago, "IdTipoPago", "NombreTipoPago");

        return View(model);
    }




}