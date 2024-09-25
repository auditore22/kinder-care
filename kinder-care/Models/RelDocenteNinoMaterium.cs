using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class RelDocenteNinoMaterium
{
    public int IdDocente { get; set; }

    public int IdNino { get; set; }

    public string Materia { get; set; } = null!;

    public virtual Docente IdDocenteNavigation { get; set; } = null!;

    public virtual Nino IdNinoNavigation { get; set; } = null!;
}
