namespace kinder_care.Models;

public partial class RelDocenteNinoMateria
{
    public int IdDocente { get; set; }

    public int IdNino { get; set; }

    public virtual Docentes IdDocenteNavigation { get; set; } = null!;

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}
