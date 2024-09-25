using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class CondicionesMedica
{
    public int Id { get; set; }

    public string NombreCondicion { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Nino> IdNinos { get; set; } = new List<Nino>();
}
