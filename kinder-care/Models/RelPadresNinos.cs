namespace kinder_care.Models;

public class RelPadresNinos
{
    public int IdPadre { get; set; }

    public int IdNino { get; set; }

    public string Relacion { get; set; } = null!;

    public virtual Ninos IdNinoNavigation { get; set; } = null!;

    public virtual Usuarios IdPadreNavigation { get; set; } = null!;
}