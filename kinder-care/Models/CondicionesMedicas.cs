using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class CondicionesMedicas
{
    public int IdCondicionMedica { get; set; }

    public string NombreCondicion { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Ninos> IdNino { get; set; } = new List<Ninos>();
}
