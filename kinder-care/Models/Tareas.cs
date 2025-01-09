namespace kinder_care.Models;

public class Tareas
{
    public int IdTarea { get; set; }

    public int IdProfesor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Calificacion { get; set; } = 0;

    public DateTime FechaAsignada { get; set; } = DateTime.Now;

    public DateTime FechaEntrega { get; set; }

    public bool Activo { get; set; } = true;
    
    public byte[]? DocTareaDocente { get; set; }

    public int? Extencion { get; set; }

    public virtual TipoDoc? ExtencionNavigation { get; set; }

    // Relación con la tabla intermedia
    public virtual ICollection<RelNinoTarea> RelNinoTarea { get; set; } = new List<RelNinoTarea>();
}