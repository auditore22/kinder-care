using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Ninos
{
    public int IdNino { get; set; }

    public string Cedula { get; set; } = null!;

    public string NombreNino { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Poliza { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual ICollection<Evaluaciones> Evaluaciones { get; set; } = new List<Evaluaciones>();

    public virtual ICollection<ObservacionesDocentes> ObservacionesDocentes { get; set; } = new List<ObservacionesDocentes>();

    public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();

    public virtual ICollection<ProgresoAcademico> ProgresoAcademico { get; set; } = new List<ProgresoAcademico>();

    public virtual ICollection<RelDocenteNinoMateria> RelDocenteNinoMateria { get; set; } = new List<RelDocenteNinoMateria>();

    public virtual ICollection<RelNinoActividad> RelNinoActividad { get; set; } = new List<RelNinoActividad>();

    public virtual ICollection<RelPadresNinos> RelPadresNinos { get; set; } = new List<RelPadresNinos>();

    public virtual ICollection<Alergias> IdAlergia { get; set; } = new List<Alergias>();

    public virtual ICollection<CondicionesMedicas> IdCondicion { get; set; } = new List<CondicionesMedicas>();

    public virtual ICollection<ContactosEmergencia> IdContacto { get; set; } = new List<ContactosEmergencia>();

    public virtual ICollection<Medicamentos> IdMedicamento { get; set; } = new List<Medicamentos>();
}
