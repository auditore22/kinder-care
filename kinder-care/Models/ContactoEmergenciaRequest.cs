namespace kinder_care.Models;

public class ContactoEmergenciaRequest
{
    public int IdNino { get; set; }
    public string? NombreContacto { get; set; }
    public int? Telefono { get; set; }
    public string? Relacion { get; set; }
    public int? IdContacto { get; set; }
    public string? Accion { get; set; }
}