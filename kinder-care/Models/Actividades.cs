namespace kinder_care.Models;

public class Actividades
{
    public int IdActividad { get; set; }

    public int IdTipoActividad { get; set; }

    public DateTime Fecha { get; set; }

    public string? Lugar { get; set; }

    public string? Descripcion { get; set; } // Nueva propiedad Descripcion

    public bool? Activo { get; set; }

    public virtual TipoActividad IdTipoActividadNavigation { get; set; } = null!;

    public virtual ICollection<RelNinoActividad> RelNinoActividad { get; set; } = new List<RelNinoActividad>();
}