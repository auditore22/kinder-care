using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class TipoPagos
{
    public int IdTipoPago { get; set; }

    public string NombreTipoPago { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();
}
