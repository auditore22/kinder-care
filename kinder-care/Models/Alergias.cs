namespace kinder_care.Models;

public partial class Alergias
{
    public int IdAlergia { get; set; }

    public string NombreAlergia { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<RelNinoAlergia> RelNinoAlergia { get; set; } = new List<RelNinoAlergia>();
}