using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class RelNinoActividad
{
    public int IdNino { get; set; }

    public int IdActividad { get; set; }

    public string Asistencia { get; set; } = null!;

    public virtual Actividade IdActividadNavigation { get; set; } = null!;

    public virtual Nino IdNinoNavigation { get; set; } = null!;
}
