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

    public IActionResult FinanceDetails()
    {
        ViewBag.CurrentSection = "FinanceDetails";
        return View();
    }

    // GET: PaymentsSearch
    [HttpGet]
    public async Task<IActionResult> PaymentsSearch(string searchQuery)
    {
        ViewBag.SearchQuery = searchQuery; // Para mostrar el valor de búsqueda en el input

        // Filtrar pagos por nombre del padre y ordenarlos por fecha más reciente
        var pagos = string.IsNullOrWhiteSpace(searchQuery)
            ? await _context.Pagos
                .Include(p => p.Nino)
                .Include(p => p.Padre)
                .Include(p => p.TipoPago)
                .OrderByDescending(p => p.FechaPago) // Ordena por fecha de pago descendente
                .ToListAsync()
            : await _context.Pagos
                .Include(p => p.Nino)
                .Include(p => p.Padre)
                .Include(p => p.TipoPago)
                .Where(p => p.Padre != null && p.Padre.Nombre.Contains(searchQuery))
                .OrderByDescending(p => p.FechaPago) // Ordena por fecha de pago descendente
                .ToListAsync();

        return View("ManagePayments", pagos);
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

        var pagos = await _context.Pagos
            .Include(p => p.Nino)
            .Include(p => p.Padre)
            .Include(p => p.TipoPago)
            .OrderByDescending(p => p.FechaPago) // Ordena por fecha de pago descendente
            .ToListAsync();

        return View(pagos);
    }

    [HttpGet]
    public async Task<IActionResult> CreatePayment()
    {
        var ninos = await _context.Ninos
            .Where(n => n.Activo == true)
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
            .Where(n => n.Activo == true)
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

    // Método para ver los detalles del pago
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var pago = await _context.Pagos
            .Include(p => p.Nino)
            .Include(p => p.Padre)
            .Include(p => p.TipoPago)
            .FirstOrDefaultAsync(p => p.IdPago == id);

        if (pago == null) return NotFound();

        return View(pago);
    }

    // GET: Payments
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        // Buscar el pago por ID
        var pago = await _context.Pagos
            .Include(p => p.Nino) // Incluir la relación con el niño
            .Include(p => p.Padre) // Incluir la relación con el padre
            .Include(p => p.TipoPago) // Incluir la relación con el tipo de pago
            .FirstOrDefaultAsync(p => p.IdPago == id);

        if (pago == null)
        {
            ViewBag.ErrorMessage = "No se encontró el pago para editar.";
            return RedirectToAction("ManagePayments");
        }

        // Mapear el pago al PaymentViewModel
        var model = new PaymentViewModel
        {
            IdPago = pago.IdPago,
            IdNino = pago.IdNino,
            IdPadre = pago.IdPadre,
            IdTipoPago = pago.IdTipoPago,
            FechaPago = pago.FechaPago,
            Monto = pago.Monto,
            MetodoPago = pago.MetodoPago,
            ReferenciaFactura = pago.ReferenciaFactura
        };

        // Pasar los datos de los campos informativos a la vista
        ViewBag.Nino = pago.Nino?.NombreNino ?? "N/A";
        ViewBag.Padre = pago.Padre?.Nombre ?? "N/A";
        ViewBag.TipoPago = pago.TipoPago?.NombreTipoPago ?? "N/A";

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PaymentViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Buscar el pago por ID
            var pago = await _context.Pagos.FindAsync(model.IdPago);
            if (pago == null)
            {
                ViewBag.ErrorMessage = "No se encontró el pago para actualizar.";
                return RedirectToAction("ManagePayments");
            }

            // Actualizar solo los campos editables
            pago.FechaPago = model.FechaPago;
            pago.Monto = model.Monto;
            pago.MetodoPago = model.MetodoPago;
            pago.ReferenciaFactura = model.ReferenciaFactura;
            pago.UltimaActualizacion = DateTime.Now;

            try
            {
                _context.Update(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManagePayments");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al actualizar el pago: {ex.Message}");
                ViewBag.ErrorMessage = "Error al actualizar el pago. Intenta de nuevo.";
                return View(model);
            }
        }

        ViewBag.ErrorMessage = "Hay errores en el formulario.";
        return View(model);
    }


    // Método para eliminar el pago
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pago = await _context.Pagos
            .Include(p => p.Nino)
            .Include(p => p.Padre)
            .Include(p => p.TipoPago)
            .FirstOrDefaultAsync(m => m.IdPago == id);

        if (pago == null)
        {
            return NotFound();
        }

        return View(pago);
    }

    // Confirmar la eliminación del pago
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);

        if (pago == null)
        {
            ViewBag.ErrorMessage = "El pago no se encontró o ya fue eliminado.";
            return RedirectToAction("ManagePayments");
        }

        try
        {
            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            // Redirige a la vista de gestión de pagos después de la eliminación exitosa
            return RedirectToAction("ManagePayments");
        }
        catch (Exception ex)
        {
            // Capturar cualquier error en la eliminación
            Console.WriteLine($"Error al eliminar el pago: {ex.Message}");
            ViewBag.ErrorMessage = "Hubo un error al intentar eliminar el pago. Por favor, inténtalo de nuevo.";
            return RedirectToAction("ManagePayments");
        }
    }
}