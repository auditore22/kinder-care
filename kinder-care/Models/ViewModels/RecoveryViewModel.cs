using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels;

public class RecoveryViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
    public string? Email { get; set; }
}