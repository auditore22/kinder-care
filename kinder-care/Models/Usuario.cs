using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models;

public partial class Usuario
{
    public int Id { get; set; }

    [MinLength(5, ErrorMessage = "El nombre de usuario debe contener minimo 5 caracteres")]
    [MaxLength(30, ErrorMessage = "El nombre de usuario debe contener maximo 30 caracteres")]
    public string Nombre { get; set; } = null!;

    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,25}$", ErrorMessage = "La contrasena debe estar entre 8 y 25 caracteres, tambien contener minimo una letra y un numero")]
    public string ContrasenaHash { get; set; } = null!;

    public int IdRol { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Docente> Docentes { get; set; } = new List<Docente>();

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Padre> Padres { get; set; } = new List<Padre>();
}
