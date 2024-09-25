using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwEvaluacionesPorNino
{
    public int Id { get; set; }

    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public string Asignatura { get; set; } = null!;

    public decimal? Puntaje { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Comentarios { get; set; }
}
