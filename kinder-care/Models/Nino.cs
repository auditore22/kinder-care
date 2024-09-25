using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class Nino
{
    public int Id { get; set; }

    public string NombreNino { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Poliza { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<Evaluacione> Evaluaciones { get; set; } = new List<Evaluacione>();

    public virtual ICollection<ObservacionesDocente> ObservacionesDocentes { get; set; } = new List<ObservacionesDocente>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<ProgresoAcademico> ProgresoAcademicos { get; set; } = new List<ProgresoAcademico>();

    public virtual ICollection<RelDocenteNinoMaterium> RelDocenteNinoMateria { get; set; } = new List<RelDocenteNinoMaterium>();

    public virtual ICollection<RelNinoActividad> RelNinoActividads { get; set; } = new List<RelNinoActividad>();

    public virtual ICollection<RelPadresNino> RelPadresNinos { get; set; } = new List<RelPadresNino>();

    public virtual ICollection<Alergia> IdAlergia { get; set; } = new List<Alergia>();

    public virtual ICollection<CondicionesMedica> IdCondicions { get; set; } = new List<CondicionesMedica>();

    public virtual ICollection<ContactosEmergencium> IdContactos { get; set; } = new List<ContactosEmergencium>();

    public virtual ICollection<Medicamento> IdMedicamentos { get; set; } = new List<Medicamento>();
}
