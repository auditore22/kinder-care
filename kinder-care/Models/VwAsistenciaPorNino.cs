using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class VwAsistenciaPorNino
{
    public int Id { get; set; }

    public int IdNino { get; set; }

    public string NombreNino { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public TimeOnly? HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    public string? Estado { get; set; }
}
