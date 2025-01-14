namespace kinder_care.Models;

public class TipoDoc
{
    public int IdDoc { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Tareas> Tareas { get; set; } = new List<Tareas>();
}