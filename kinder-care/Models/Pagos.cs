using System;
using System.Collections.Generic;

namespace kinder_care.Model;

public partial class Pagos
{
    public int IdPago { get; set; }

    public int IdNino { get; set; }

    public int IdPadre { get; set; }

    public int IdTipoPago { get; set; }

    public DateOnly FechaPago { get; set; }

    public decimal Monto { get; set; }

    public string? MetodoPago { get; set; }

    public string? ReferenciaFactura { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public virtual Ninos IdNinoNavigation { get; set; } = null!;

    public virtual Usuarios IdPadreNavigation { get; set; } = null!;

    public virtual TipoPagos IdTipoPagoNavigation { get; set; } = null!;
}
