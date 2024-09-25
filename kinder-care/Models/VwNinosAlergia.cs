using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwNinosAlergia
{
    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public string NombreAlergia { get; set; } = null!;
}
