using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class TipoDoc
{
    public int IdDoc { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Tareas> Tareas { get; set; } = new List<Tareas>();
}
