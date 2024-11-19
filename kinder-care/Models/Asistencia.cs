using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Asistencia
{
    public int IdAsistencia { get; set; }

    public int IdNino { get; set; }

    public DateTime Fecha { get; set; }

    public DateTime HoraEntrada { get; set; }

    public TimeSpan? HoraSalida { get; set; }

    public string Estado { get; set; }

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}
