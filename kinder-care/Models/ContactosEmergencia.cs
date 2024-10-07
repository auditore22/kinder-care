using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class ContactosEmergencia
{
    public int IdContactoEmergencia { get; set; }

    public string NombreContacto { get; set; } = null!;

    public string? Relacion { get; set; }

    public int? Telefono { get; set; }

    public string? Direccion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Ninos> IdNino { get; set; } = new List<Ninos>();
}
