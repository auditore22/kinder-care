namespace kinder_care.Models;

public class Documentos
{
    public int IdDoc { get; set; }

    public byte[]? Documento1 { get; set; }

    public virtual ICollection<RelNinoTarea> RelNinoTareas { get; set; } = new List<RelNinoTarea>();

    public virtual ICollection<Tareas> Tareas { get; set; } = new List<Tareas>();
}