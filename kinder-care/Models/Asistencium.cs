using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Asistencium
{
    public int Id { get; set; }

    public int IdNino { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly? HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    public string? Estado { get; set; }

    public virtual Nino IdNinoNavigation { get; set; } = null!;
}
