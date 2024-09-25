using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwUsuariosPorRol
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
