using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels;

public class PaymentViewModel
{
    [Required(ErrorMessage = "El campo 'IdPago' es requerido.")]
    public int IdPago { get; set; }

    [Required(ErrorMessage = "El campo 'IdNino' es requerido.")]
    public int IdNino { get; set; }

    [Required(ErrorMessage = "El campo 'IdPadre' es requerido.")]
    public int IdPadre { get; set; }

    [Required(ErrorMessage = "El campo 'IdTipoPago' es requerido.")]
    public int IdTipoPago { get; set; }

    [Required(ErrorMessage = "El campo 'FechaPago' es requerido.")]
    public DateTime FechaPago { get; set; }

    [Required(ErrorMessage = "El campo 'Monto' es requerido.")]
    [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0 y menor de 999.999.99.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "El campo 'Método de Pago' es requerido.")]
    [MaxLength(30, ErrorMessage = "El método de pago debe contener maximo 20 caracteres")]
    public string? MetodoPago { get; set; }

    [MaxLength(30, ErrorMessage = "La referencia debe contener maximo 15 caracteres")]
    public string? ReferenciaFactura { get; set; }
}