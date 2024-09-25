using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwReporteProgresoAcademico
{
    public int IdNino { get; set; }

    public string AreaAcademica { get; set; } = null!;

    public string? NivelProgreso { get; set; }

    public string? Descripcion { get; set; }

    public string? Asignatura { get; set; }

    public decimal? Puntaje { get; set; }

    public DateOnly? FechaEvaluacion { get; set; }

    public string? ObservacionDocente { get; set; }
}
