using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "La contraseña actual es requerida.")]
    public string CurrentPassword { get; set; } = null!;

    [Required(ErrorMessage = "La nueva contraseña es requerida.")]
    [MinLength(8, ErrorMessage = "La nueva contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(25, ErrorMessage = "La nueva contraseña no puede tener más de 25 caracteres.")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "La confirmación de la contraseña es requerida.")]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmPassword { get; set; } = null!;
}