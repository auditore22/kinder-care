using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwExpediente
{
    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public string? AreaAcademica { get; set; }

    public string? NivelProgreso { get; set; }

    public string? DescripcionProgreso { get; set; }

    public DateTime? FechaProgreso { get; set; }

    public string? Asignatura { get; set; }

    public decimal? Puntaje { get; set; }

    public DateOnly? FechaEvaluacion { get; set; }

    public string? ComentariosEvaluacion { get; set; }

    public string? DescripcionObservacion { get; set; }

    public DateOnly? FechaObservacion { get; set; }

    public DateOnly? FechaActividad { get; set; }

    public string? NombreTipoActividad { get; set; }

    public string? Asistencia { get; set; }
}
