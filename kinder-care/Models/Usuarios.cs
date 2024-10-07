using System;
using System.Collections.Generic;

namespace kinder_care.Model;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ContrasenaHash { get; set; } = null!;

    public int? NumTelefono { get; set; }

    public string Direccion { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public int IdRol { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Docentes> Docentes { get; set; } = new List<Docentes>();

    public virtual Roles IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();

    public virtual ICollection<RelPadresNinos> RelPadresNinos { get; set; } = new List<RelPadresNinos>();
}
