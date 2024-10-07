using System;
using System.Collections.Generic;

namespace kinder_care.Model;

public partial class RelDocenteNinoMateria
{
    public int IdDocente { get; set; }

    public int IdNino { get; set; }

    public string Materia { get; set; } = null!;

    public virtual Docentes IdDocenteNavigation { get; set; } = null!;

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}
