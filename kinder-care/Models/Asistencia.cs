using System;
using System.Collections.Generic;

namespace kinder_care.Model;

public partial class Asistencia
{
    public int IdAsistencia { get; set; }

    public int IdNino { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly? HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    public string? Estado { get; set; }

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}
