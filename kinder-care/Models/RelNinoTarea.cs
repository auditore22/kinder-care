namespace kinder_care.Models;

public class RelNinoTarea
{
    public int IdNino { get; set; }

    public int IdTarea { get; set; }

    public int? Calificacion { get; set; } = 0;

    // Relación con el documento del niño
    public int? IdDocNino { get; set; }

    public virtual Documentos? DocTareaNino { get; set; }

    // Propiedades de navegación
    public virtual Ninos Ninos { get; set; } = null!;

    public virtual Tareas Tareas { get; set; } = null!;
}