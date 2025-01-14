namespace kinder_care.Models;

public class RelNinoCondicion
{
    public int IdNino { get; set; }
    public Ninos? Nino { get; set; } // Navegación hacia la entidad Ninos

    public int IdCondicion { get; set; }
    public CondicionesMedicas? Condicion { get; set; } // Navegación hacia la entidad CondicionesMedicas
}