using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels;

public class PaymentViewModel
{
    [Required(ErrorMessage = "El campo 'IdNino' es requerido.")]
    public int IdNino { get; set; }

    [Required(ErrorMessage = "El campo 'IdPadre' es requerido.")]
    public int IdPadre { get; set; }

    [Required(ErrorMessage = "El campo 'IdTipoPago' es requerido.")]
    public int IdTipoPago { get; set; }

    [Required(ErrorMessage = "El campo 'FechaPago' es requerido.")]
    public DateTime FechaPago { get; set; }

    [Required(ErrorMessage = "El campo 'Monto' es requerido.")]
    [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "El campo 'Metodo de Pago' es requerido.")]
    public string? MetodoPago { get; set; }

    public string? ReferenciaFactura { get; set; }
}