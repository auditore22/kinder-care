using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwNinosPorDocente
{
    public int IdDocente { get; set; }

    public string NombreDocente { get; set; } = null!;

    public string ApellidoDocente { get; set; } = null!;

    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public string Materia { get; set; } = null!;
}
