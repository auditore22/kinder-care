namespace kinder_care.Models;

public class Asistencia
{
    public int IdAsistencia { get; set; }

    public int IdNino { get; set; }

    public DateTime Fecha { get; set; }

    public bool? Presente { get; set; }

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}