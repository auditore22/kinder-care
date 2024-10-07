using System;
using System.Collections.Generic;

namespace kinder_care.Model;

public partial class Medicamentos
{
    public int IdMedicamento { get; set; }

    public string NombreMedicamento { get; set; } = null!;

    public string Dosis { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Ninos> IdNino { get; set; } = new List<Ninos>();
}
