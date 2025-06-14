﻿namespace kinder_care.Models;

public class TipoActividad
{
    public int IdTipoActividad { get; set; }

    public string NombreTipoActividad { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Actividades> Actividades { get; set; } = new List<Actividades>();
}