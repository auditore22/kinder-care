using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwPagosPorPadre
{
    public int Id { get; set; }

    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public int IdPadre { get; set; }

    public string NombrePadre { get; set; } = null!;

    public decimal Monto { get; set; }

    public string NombreTipoPago { get; set; } = null!;

    public string? MetodoPago { get; set; }

    public string? ReferenciaFactura { get; set; }

    public DateOnly FechaPago { get; set; }
}
