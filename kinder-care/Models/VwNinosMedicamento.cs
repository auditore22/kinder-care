using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwNinosMedicamento
{
    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public string NombreMedicamento { get; set; } = null!;

    public string Dosis { get; set; } = null!;
}
