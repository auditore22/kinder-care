using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class ObservacionesDocentes
{
    public int IdObservacionDocente { get; set; }

    public int IdNino { get; set; }

    public int IdDocente { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual Docentes IdDocenteNavigation { get; set; } = null!;

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}
