using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Docente
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public string? GrupoAsignado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ObservacionesDocente> ObservacionesDocentes { get; set; } = new List<ObservacionesDocente>();

    public virtual ICollection<RelDocenteNinoMaterium> RelDocenteNinoMateria { get; set; } = new List<RelDocenteNinoMaterium>();
}
