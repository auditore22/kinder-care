namespace kinder_care.Models;

public class RelNinoTarea
{
    public int IdNino { get; set; }

    public int IdTarea { get; set; }

    public int? Calificacion { get; set; } = 0;

    public int? Id_Doc { get; set; }

    public byte[]? DocTareaNino { get; set; }

    // Propiedades de navegación
    public virtual Ninos Ninos { get; set; } = null!;

    public virtual Tareas Tareas { get; set; } = null!;

    public virtual TipoDoc? TipoDoc { get; set; }
}