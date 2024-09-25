using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwRelPadresNino
{
    public int IdPadre { get; set; }

    public int IdUsuarioPadre { get; set; }

    public string NombrePadre { get; set; } = null!;

    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public string Relacion { get; set; } = null!;
}
