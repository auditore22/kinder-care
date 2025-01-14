namespace kinder_care.Models;

public class InformacionMedicaRequest
{
    public int IdNino { get; set; }
    public int? IdAlergia { get; set; }
    public int? IdCondicion { get; set; }
    public int? IdMedicamento { get; set; }
    public string? Accion { get; set; }
}