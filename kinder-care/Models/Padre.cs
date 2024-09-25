using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Padre
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<RelPadresNino> RelPadresNinos { get; set; } = new List<RelPadresNino>();
}
