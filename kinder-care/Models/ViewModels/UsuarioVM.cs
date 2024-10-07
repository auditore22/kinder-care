using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels
{
    public class UsuarioVM
    {
        [Display(Name = "Nombre de Usuario")]
        [MinLength(5, ErrorMessage = "El nombre de usuario debe contener minimo 5 caracteres")]
        [MaxLength(30, ErrorMessage = "El nombre de usuario debe contener maximo 30 caracteres")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,25}$", ErrorMessage = "La contrasena debe estar entre 8 y 25 caracteres, tambien contener minimo una letra y un numero")]
        public string ContrasenaHash { get; set; } = null!;
    }
}
