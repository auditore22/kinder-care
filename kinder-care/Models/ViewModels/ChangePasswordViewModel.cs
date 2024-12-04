using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "La contraseña actual es requerida.")]
    public string CurrentPassword { get; set; } = null!;

    [Required(ErrorMessage = "La nueva contraseña es requerida.")]
    [MinLength(8, ErrorMessage = "La nueva contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(25, ErrorMessage = "La nueva contraseña no puede tener más de 25 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[0-9@#$%^&+=!]).{8,25}$", 
        ErrorMessage = "La nueva contraseña debe tener entre 8 y 25 caracteres, incluir al menos una letra minúscula y un número o símbolo.")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "La confirmación de la contraseña es requerida.")]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmPassword { get; set; } = null!;
}