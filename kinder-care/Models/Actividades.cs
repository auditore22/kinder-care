using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Actividades
{
    public int IdActividad { get; set; }

    public int IdTipoActividad { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Lugar { get; set; }

    public bool? Activo { get; set; }

    public virtual TipoActividad IdTipoActividadNavigation { get; set; } = null!;

    public virtual ICollection<RelNinoActividad> RelNinoActividad { get; set; } = new List<RelNinoActividad>();
}
