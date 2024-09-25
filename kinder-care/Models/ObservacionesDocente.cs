using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class ObservacionesDocente
{
    public int Id { get; set; }

    public int IdNino { get; set; }

    public int IdDocente { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual Docente IdDocenteNavigation { get; set; } = null!;

    public virtual Nino IdNinoNavigation { get; set; } = null!;
}
