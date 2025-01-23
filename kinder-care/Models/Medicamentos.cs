namespace kinder_care.Models;

public class Medicamentos
{
    public int IdMedicamento { get; set; }

    public string NombreMedicamento { get; set; } = null!;

    public string Dosis { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<RelNinoMedicamento> RelNinoMedicamento { get; set; } = new List<RelNinoMedicamento>();
}