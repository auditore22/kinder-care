using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class ContactosEmergencium
{
    public int Id { get; set; }

    public string NombreContacto { get; set; } = null!;

    public string? Relacion { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Nino> IdNinos { get; set; } = new List<Nino>();
}
