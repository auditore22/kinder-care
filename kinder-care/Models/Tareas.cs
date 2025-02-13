namespace kinder_care.Models;

public class Tareas
{
    public int IdTarea { get; set; }

    public int IdProfesor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaAsignada { get; set; } = DateTime.Now;

    public DateTime FechaEntrega { get; set; }

    public bool Activo { get; set; } = true;

    // Relación con el documento del docente
    public int? IdDocDocente { get; set; }

    public virtual Documentos? IdDocDocenteNavigation { get; set; }

    // Relación con la tabla intermedia
    public virtual ICollection<RelNinoTarea> RelNinoTarea { get; set; } = new List<RelNinoTarea>();
}