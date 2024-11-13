namespace kinder_care.Models;

public class RelNinoAlergia
{
    public int IdNino { get; set; }
    public virtual Ninos Nino { get; set; }  // Navegación hacia la entidad Ninos

    public int IdAlergia { get; set; }
    public virtual Alergias Alergia { get; set; }  // Navegación hacia la entidad Alergias
}