namespace kinder_care.Models;

public class Documentos
{
    public int IdDoc { get; set; }

    public byte[]? Documento { get; set; }
    
    public string? Nombre { get; set; } // Nombre original del archivo (ej. "Tarea1.pdf")

    public string? Tipo { get; set; } // Tipo MIME del archivo (ej. "application/pdf", "image/jpeg")

    public virtual ICollection<RelNinoTarea> RelNinoTareas { get; set; } = new List<RelNinoTarea>();

    public virtual ICollection<Tareas> Tareas { get; set; } = new List<Tareas>();
}