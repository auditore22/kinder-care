using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels;

public class EventViewModel
{
    public int IdActividad { get; set; }

    [Required(ErrorMessage = "La fecha y hora es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El lugar es obligatorio.")]
    [MinLength(5, ErrorMessage = "El lugar debe contener minimo 3 caracteres")]
    [MaxLength(30, ErrorMessage = "El lugar debe contener maximo 30 caracteres")]
    public string? Lugar { get; set; }

    [Required(ErrorMessage = "El tipo de actividad es obligatorio.")]
    public int IdTipoActividad { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [MaxLength(100, ErrorMessage = "La descripcion debe contener maximo 100 caracteres")]
    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    // Propiedad para mostrar el nombre del tipo de actividad
    public string? NombreTipoActividad { get; set; }
}