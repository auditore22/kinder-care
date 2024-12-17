using System.ComponentModel.DataAnnotations;

namespace kinder_care.Models.ViewModels
{
    public class EventViewModel
    {
        public int IdActividad { get; set; }

        [Required(ErrorMessage = "La fecha y hora es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El lugar es obligatorio.")]
        [StringLength(100, ErrorMessage = "El lugar no puede superar los 100 caracteres.")]
        public string? Lugar { get; set; }

        [Required(ErrorMessage = "El tipo de actividad es obligatorio.")]
        public int IdTipoActividad { get; set; }

        public string? Descripcion { get; set; }

        public bool? Activo { get; set; }

        // Propiedad para mostrar el nombre del tipo de actividad
        public string? NombreTipoActividad { get; set; }
    }
}