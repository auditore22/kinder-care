namespace kinder_care.Models;

public partial class ProgresoAcademico
{
    public int IdProgresoAcademico { get; set; }

    public int IdNino { get; set; }

    public string AreaAcademica { get; set; } = null!;

    public string? NivelProgreso { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public virtual Ninos IdNinoNavigation { get; set; } = null!;
}
