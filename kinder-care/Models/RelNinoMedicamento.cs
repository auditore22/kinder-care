namespace kinder_care.Models;

public class RelNinoMedicamento
{
    public int IdNino { get; set; }
    public Ninos Nino { get; set; }  // Navegación hacia la entidad Ninos

    public int IdMedicamento { get; set; }
    public Medicamentos Medicamento { get; set; }  // Navegación hacia la entidad Medicamentos
}