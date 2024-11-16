using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    [Display(Name = "Cédula")]
    [RegularExpression(@"^\d+$", ErrorMessage = "La cédula solo puede contener números.")]
    public string Cedula { get; set; } = null!;

    [Display(Name = "Nombre de Usuario")]
    [MinLength(5, ErrorMessage = "El nombre de usuario debe contener minimo 5 caracteres")]
    [MaxLength(30, ErrorMessage = "El nombre de usuario debe contener maximo 30 caracteres")]
    public string Nombre { get; set; } = null!;

    [Display(Name = "Contraseña")]
    [RegularExpression(@"^(?=.[A-Za-z])(?=.\d)[A-Za-z\d]{8,25}$",
        ErrorMessage =
            "La contrasena debe estar entre 8 y 25 caracteres, tambien contener minimo una letra y un numero")]
    public string ContrasenaHash { get; set; } = null!;

    [Display(Name = "Número de Teléfono")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "El número de teléfono debe contener exactamente 8 dígitos.")]
    public int NumTelefono { get; set; }

    [Display(Name = "Dirección")]
    [MaxLength(200, ErrorMessage = "Ingrese su direccion sin exeder los 200 caracteres")]
    public string Direccion { get; set; } = null!;

    [Display(Name = "Correo Electrónico")]
    [EmailAddress(ErrorMessage = "Ingrese un email valido")]
    public string CorreoElectronico { get; set; } = null!;

    public string TokenRecovery { get; set; }

    public int IdRol { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Docentes> Docentes { get; set; } = new List<Docentes>();

    public virtual Roles IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();

    public virtual ICollection<RelPadresNinos> RelPadresNinos { get; set; } = new List<RelPadresNinos>();
}