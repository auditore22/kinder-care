using System;
using System.Collections.Generic;

namespace kinder_care.Model;

public partial class RelNinoActividad
{
    public int IdNino { get; set; }

    public int IdActividad { get; set; }

    public string Asistencia { get; set; } = null!;

    public virtual Actividades IdActividadNavigation { get; set; } = null!;

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}
