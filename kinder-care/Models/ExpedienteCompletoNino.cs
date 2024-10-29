namespace kinder_care.Models;

public class ExpedienteCompletoNino
{
    public int IdNino { get; set; }
    public string? Cedula { get; set; }
    public string? NombreNino { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string? Direccion { get; set; }
    public string? Poliza { get; set; } 
    public string? NombreAlergia { get; set; }
    public string? NombreCondicion { get; set; }
    public string? NombreMedicamento { get; set; }
    public string? Dosis { get; set; }
    public string? NombreContacto { get; set; }
    public string? TelefonoContacto { get; set; }
    public string? RelacionContacto { get; set; }
}
