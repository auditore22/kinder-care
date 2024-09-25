using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwContactosEmergenciaPorNino
{
    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public string NombreContacto { get; set; } = null!;

    public string? Relacion { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }
}
