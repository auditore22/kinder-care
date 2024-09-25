using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwNinosPorNombre
{
    public int Id { get; set; }

    public string NombreNino { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Poliza { get; set; }
}
