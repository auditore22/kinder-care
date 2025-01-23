using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kinder_care.Models;

public class Pagos
{
    [Key] public int IdPago { get; set; }

    [Required(ErrorMessage = "El campo 'IdNino' es requerido.")]
    public int IdNino { get; set; }

    [ForeignKey("IdNino")] public virtual Ninos? Nino { get; set; } // Propiedad de navegación opcional

    [Required(ErrorMessage = "El campo 'IdPadre' es requerido.")]
    public int IdPadre { get; set; }

    [ForeignKey("IdPadre")] public virtual Usuarios? Padre { get; set; } // Propiedad de navegación opcional

    [Required(ErrorMessage = "El campo 'IdTipoPago' es requerido.")]
    public int IdTipoPago { get; set; }

    [ForeignKey("IdTipoPago")] public virtual TipoPagos? TipoPago { get; set; } // Propiedad de navegación opcional

    [Required(ErrorMessage = "El campo 'FechaPago' es requerido.")]
    public DateTime FechaPago { get; set; }

    [Required(ErrorMessage = "El campo 'Monto' es requerido.")]
    [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0.")]
    public decimal Monto { get; set; }

    public string? MetodoPago { get; set; }

    public string? ReferenciaFactura { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }
}