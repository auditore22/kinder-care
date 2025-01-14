namespace kinder_care.Models;

public class Niveles
{
    public int IdNivel { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Ninos> Ninos { get; set; } = new List<Ninos>();
}