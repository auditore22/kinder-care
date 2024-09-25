using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwNinosCondicionesMedica
{
    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public string NombreCondicion { get; set; } = null!;
}
