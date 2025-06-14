﻿namespace kinder_care.Models;

public class Docentes
{
    public int IdDocente { get; set; }

    public int IdUsuario { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool Activo { get; set; }

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ObservacionesDocentes> ObservacionesDocentes { get; set; } =
        new List<ObservacionesDocentes>();

    public virtual ICollection<RelDocenteNinoMateria> RelDocenteNinoMateria { get; set; } =
        new List<RelDocenteNinoMateria>();
}