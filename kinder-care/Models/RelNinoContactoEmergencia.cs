namespace kinder_care.Models;

public class RelNinoContactoEmergencia
{
    public int IdNino { get; set; }
    public int IdContacto { get; set; }

    public virtual Ninos Nino { get; set; }
    public virtual ContactosEmergencia ContactoEmergencia { get; set; }
}