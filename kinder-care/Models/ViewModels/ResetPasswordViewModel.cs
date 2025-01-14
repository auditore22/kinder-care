using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels;

public class ResetPasswordViewModel
{
    [Required] public string? Email { get; set; }

    [Required] public string? Token { get; set; }

    [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(25, ErrorMessage = "La nueva contraseña no puede tener más de 25 caracteres.")]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(25, ErrorMessage = "La nueva contraseña no puede tener más de 25 caracteres.")]
    public string? ConfirmPassword { get; set; }
}