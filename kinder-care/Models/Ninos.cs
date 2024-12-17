using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models;

public partial class Ninos
{
    public int IdNino { get; set; }

    [Display(Name = "Cédula")]
    [MinLength(9, ErrorMessage = "El nombre de usuario debe contener minimo 9 caracteres")]
    [MaxLength(9, ErrorMessage = "El nombre de usuario debe contener maximo 9 caracteres")]
    public string Cedula { get; set; } = null!;

    [Display(Name = "Nombre Completo")]
    [MinLength(5, ErrorMessage = "El nombre de usuario debe contener minimo 5 caracteres")]
    [MaxLength(30, ErrorMessage = "El nombre de usuario debe contener maximo 30 caracteres")]
    public string NombreNino { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    [Display(Name = "Dirección")]
    [MaxLength(200, ErrorMessage = "Ingrese su direccion sin exeder los 200 caracteres")]
    public string Direccion { get; set; } = null!;

    public string? Poliza { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual ICollection<Evaluaciones> Evaluaciones { get; set; } = new List<Evaluaciones>();

    public virtual ICollection<ObservacionesDocentes> ObservacionesDocentes { get; set; } =
        new List<ObservacionesDocentes>();

    public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();

    public virtual ICollection<ProgresoAcademico> ProgresoAcademico { get; set; } = new List<ProgresoAcademico>();

    public virtual ICollection<RelDocenteNinoMateria> RelDocenteNinoMateria { get; set; } =
        new List<RelDocenteNinoMateria>();

    public virtual ICollection<RelNinoActividad> RelNinoActividad { get; set; } = new List<RelNinoActividad>();

    public virtual ICollection<RelPadresNinos> RelPadresNinos { get; set; } = new List<RelPadresNinos>();

    public virtual ICollection<RelNinoAlergia> RelNinoAlergia { get; set; } = new List<RelNinoAlergia>();
    public virtual ICollection<RelNinoMedicamento> RelNinoMedicamento { get; set; } = new List<RelNinoMedicamento>();
    public virtual ICollection<RelNinoCondicion> RelNinoCondicion { get; set; } = new List<RelNinoCondicion>();

    public virtual ICollection<RelNinoContactoEmergencia> RelNinoContactoEmergencia { get; set; } =
        new List<RelNinoContactoEmergencia>();

    public virtual ICollection<RelNinoTarea> RelNinoTarea { get; set; } = new List<RelNinoTarea>();
}