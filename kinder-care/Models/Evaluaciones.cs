using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Evaluaciones
{
    public int IdEvaluacion { get; set; }

    public int IdNino { get; set; }

    public string Asignatura { get; set; } = null!;

    public decimal? Puntaje { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Comentarios { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}
