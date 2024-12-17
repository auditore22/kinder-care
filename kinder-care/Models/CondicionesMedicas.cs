namespace kinder_care.Models;

public partial class CondicionesMedicas
{
    public int IdCondicionMedica { get; set; }

    public string NombreCondicion { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<RelNinoCondicion> RelNinoCondicion { get; set; } = new List<RelNinoCondicion>();
}