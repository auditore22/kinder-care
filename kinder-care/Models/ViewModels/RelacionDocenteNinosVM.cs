namespace kinder_care.Models.ViewModels
{
    public class RelacionDocenteNinosVM
    {
        public int IdDocente { get; set; } 
        public List<Docentes> Docentes { get; set; } = new List<Docentes>(); 
        public List<Ninos> Ninos { get; set; } = new List<Ninos>(); 
        public List<int> NinosSeleccionados { get; set; } = new List<int>();
    }
}
