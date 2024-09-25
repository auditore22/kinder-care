using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwActividadesNino
{
    public int IdNino { get; set; }

    public DateOnly Fecha { get; set; }

    public string NombreTipoActividad { get; set; } = null!;

    public string Asistencia { get; set; } = null!;
}
