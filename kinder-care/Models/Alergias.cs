using System;
using System.Collections.Generic;

namespace kinder_care.Model;

public partial class Alergias
{
    public int IdAlergia { get; set; }

    public string NombreAlergia { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Ninos> IdNino { get; set; } = new List<Ninos>();
}
