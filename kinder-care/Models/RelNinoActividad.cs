namespace kinder_care.Models;

public class RelNinoActividad
{
    public int IdNino { get; set; }

    public int IdActividad { get; set; }

    public string Asistencia { get; set; } = null!;

    public virtual Actividades IdActividadNavigation { get; set; } = null!;

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}