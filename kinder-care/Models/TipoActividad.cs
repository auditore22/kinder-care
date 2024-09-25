using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class TipoActividad
{
    public int Id { get; set; }

    public string NombreTipoActividad { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Actividade> Actividades { get; set; } = new List<Actividade>();
}
