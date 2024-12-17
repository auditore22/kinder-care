using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels
{
    public class UsuarioVM
    {
        [Display(Name = "Correo Electrónico")]
        [EmailAddress(ErrorMessage = "Ingrese un email valido")]
        public string CorreoElectronico { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,25}$",
            ErrorMessage =
                "La contrasena debe estar entre 8 y 25 caracteres, tambien contener minimo una letra y un numero")]
        public string ContrasenaHash { get; set; } = null!;
    }
}