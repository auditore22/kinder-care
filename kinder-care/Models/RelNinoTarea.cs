namespace kinder_care.Models;

public class RelNinoTarea
{
    public int IdNino { get; set; }

    public int IdTarea { get; set; }

    // Propiedades de navegación
    public virtual Ninos Ninos { get; set; } = null!;

    public virtual Tareas Tareas { get; set; } = null!;
}