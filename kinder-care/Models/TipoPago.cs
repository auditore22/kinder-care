using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class TipoPago
{
    public int Id { get; set; }

    public string NombreTipoPago { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
