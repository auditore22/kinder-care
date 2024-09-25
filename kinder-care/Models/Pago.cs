using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int IdNino { get; set; }

    public int IdPadre { get; set; }

    public int IdTipoPago { get; set; }

    public DateOnly FechaPago { get; set; }

    public decimal Monto { get; set; }

    public string? MetodoPago { get; set; }

    public string? ReferenciaFactura { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public virtual Nino IdNinoNavigation { get; set; } = null!;

    public virtual Padre IdPadreNavigation { get; set; } = null!;

    public virtual TipoPago IdTipoPagoNavigation { get; set; } = null!;
}
